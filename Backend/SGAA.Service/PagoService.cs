namespace SGAA.Service
{
    using Azure;
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System.Threading.Tasks;

    public class PagoService : IPagoService
    {
        private const int VENCIMIENTO_DAYS = 10;
        private readonly IContratoRepository _contratoRepository;
        private readonly IPagoRepository _pagoRepository;
        private readonly IPagoMapper _pagoMapper;

        public PagoService(IContratoRepository contratoRepository, IPagoRepository pagoRepository, IPagoMapper pagoMapper)
        {
            _contratoRepository = contratoRepository;
            _pagoRepository = pagoRepository;
            _pagoMapper = pagoMapper;
        }

        private static decimal CalculatePagoProporcional(decimal alquilerMensual, DateOnly fechaPagoDesde, DateOnly fechaPagoHasta)
        {
            DateTime fechaMesDesde = new(fechaPagoDesde.Year, fechaPagoDesde.Month, 1);
            DateTime fechaMesHasta = new(fechaPagoHasta.Year, fechaPagoHasta.Month, DateTime.DaysInMonth(fechaPagoHasta.Year, fechaPagoHasta.Month));
            double totalDays = (fechaMesHasta - fechaMesDesde).TotalDays + 1;
            double effectiveDays = (fechaPagoHasta.ToDateTime(TimeOnly.MinValue) - fechaPagoHasta.ToDateTime(TimeOnly.MinValue)).TotalDays + 1;
            return alquilerMensual * (decimal)(effectiveDays / totalDays);
        }

        public async Task<IReadOnlyCollection<PagoGetModel>> CreatePagosContrato(int contratoId)
        {
            Contrato contrato = await _contratoRepository.GetContrato(contratoId)
                ?? throw new NotFoundException();
            decimal alquilerMensual = contrato.MontoAlquiler;
            List<Pago> pagos = new();
            DateOnly fechaContratoDesde = contrato.FechaDesde;
            DateOnly fechaContratoHasta = contrato.FechaHasta;
            DateOnly fechaContratoFinMes = new(fechaContratoHasta.Year, fechaContratoHasta.Month, DateTime.DaysInMonth(fechaContratoHasta.Year, fechaContratoHasta.Month));

            // Depósito
            string descripcionDepósito = $"Depósito en garantía";
            Pago pagoDeposito = new(contratoId, descripcionDepósito, alquilerMensual, fechaContratoDesde, PagoStatus.Pendiente, null, null);
            pagos.Add(pagoDeposito);

            // Alquiler
            for (DateOnly fecha = fechaContratoDesde; fecha <= fechaContratoFinMes; fecha = fecha.AddMonths(1))
            {
                DateOnly fechaPagoFinMes = fecha.AddMonths(1);
                DateOnly fechaPagoDesde = fecha;
                DateOnly fechaPagoHasta = fechaPagoFinMes < fechaContratoHasta ? fechaPagoFinMes : fechaContratoHasta;
                DateOnly fechaVencimiento = fechaPagoDesde.AddDays(VENCIMIENTO_DAYS - 1);
                decimal pagoMes = CalculatePagoProporcional(alquilerMensual, fechaPagoDesde, fechaPagoHasta);
                string descripcion = $"Alquiler: {fechaContratoDesde.ToShortDateString()} hasta {fechaPagoHasta.ToShortDateString()}";
                Pago pago = new(contratoId, descripcion, pagoMes, fechaVencimiento, PagoStatus.Pendiente, null, null);
                pagos.Add(pago);
            }

            contrato.AddPagos(pagos);
            await _contratoRepository.UpdateContrato(contrato);
            return contrato.Pagos.Select(p => p.MapToGetModel(_pagoMapper)).ToList();
        }

        public async Task<PagoGetModel> AbonarPago(int pagoId, AbonarPagoPutModel model)
        {
            Pago? pago = await _pagoRepository.GetPago(pagoId);
            if (pago == null ||
                !pago.Contrato.Firmas.Any(f => f.Rol == FirmaRol.Inquilino && f.UsuarioId == model.InquilinoUsuarioId))
                throw new NotFoundException();
            pago = _pagoMapper.ToEntity(model, pago);
            pago = await _pagoRepository.UpdatePago(pago);
            return pago.MapToGetModel(_pagoMapper);
        }

        public async Task<PagoGetModel> AprobarPago(int pagoId, AprobarPagoPutModel model)
        {
            Pago? pago = await _pagoRepository.GetPago(pagoId);
            if (pago == null ||
                !pago.Contrato.Firmas.Any(f => f.Rol == FirmaRol.Propietario && f.UsuarioId == model.PropietarioUsuarioId))
                throw new NotFoundException();
            pago = _pagoMapper.ToEntity(model, pago);
            pago = await _pagoRepository.UpdatePago(pago);
            return pago.MapToGetModel(_pagoMapper);
        }

        public async Task<PagoGetModel> GetPagoByPropietario(int propietarioUsuarioId, int pagoId)
        {
            Pago? pago = await _pagoRepository.GetPago(pagoId);
            if (pago == null ||
                !pago.Contrato.Firmas.Any(f => f.Rol == FirmaRol.Propietario && f.UsuarioId == propietarioUsuarioId))
                throw new NotFoundException();
            return pago.MapToGetModel(_pagoMapper);
        }

        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagosByPropietarioAndContrato(int propietarioUsuarioId, int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            if (contrato == null ||
                !contrato.Firmas.Any(f => f.Rol == FirmaRol.Propietario && f.UsuarioId == propietarioUsuarioId))
                throw new NotFoundException();
            return contrato.Pagos
                .Select(p => p.MapToGetModel(_pagoMapper))
                .ToList();
        }

        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagosByPropietario(int propietarioUsuarioId)
        {
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratosByRol(propietarioUsuarioId, FirmaRol.Propietario);
            return contratos
                .SelectMany(c => c.Pagos)
                .Select(p => p.MapToGetModel(_pagoMapper))
                .ToList();
        }

        public async Task<PagoGetModel> AddPago(PagoPostModel model)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(model.ContratoId);
            if (contrato == null ||
                !contrato.Firmas.Any(f => f.Rol == FirmaRol.Propietario && f.UsuarioId == model.PropietarioUsuarioId))
                throw new NotFoundException();
            if (model.Monto <= 0)
                throw new BadRequestException(nameof(model.Monto), "El monto del pago debe ser mayor a 0");
            if (string.IsNullOrWhiteSpace(model.Descripcion))
                throw new BadRequestException(nameof(model.Descripcion), "La descripción no puede estar vacía");
            Pago pago = await _pagoRepository.AddPago(model.ToEntity(_pagoMapper));
            return pago.MapToGetModel(_pagoMapper);
        }

        public async Task<PagoGetModel> GetPagoByInquilino(int inquilinoUsuarioId, int pagoId)
        {
            Pago? pago = await _pagoRepository.GetPago(pagoId);
            if (pago == null ||
                !pago.Contrato.Firmas.Any(f => f.Rol == FirmaRol.Inquilino && f.UsuarioId == inquilinoUsuarioId))
                throw new NotFoundException();
            return pago.MapToGetModel(_pagoMapper);
        }

        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagosByInquilino(int inquilinoUsuarioId)
        {
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratosByRol(inquilinoUsuarioId, FirmaRol.Inquilino);
            return contratos
                .SelectMany(c => c.Pagos)
                .Select(p => p.MapToGetModel(_pagoMapper))
                .ToList();
        }

        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagosByInquilinoAndContrato(int inquilinoUsuarioId, int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            if (contrato == null ||
                !contrato.Firmas.Any(f => f.Rol == FirmaRol.Inquilino && f.UsuarioId == inquilinoUsuarioId))
                throw new NotFoundException();
            return contrato.Pagos
                .Select(p => p.MapToGetModel(_pagoMapper))
                .ToList();
        }
    }
}
