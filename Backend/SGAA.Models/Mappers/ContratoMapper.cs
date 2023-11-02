namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class ContratoMapper : IContratoMapper
    {
        public ContratoGetModel ToGetModel(Contrato entity)
        =>
            new()
            {
                Id = entity.Id,
                PostulacionId = entity.Postulacion.Id,
                AplicacionId = entity.Postulacion.AplicacionId,
                FechaCancelacion = entity.FechaCancelacion,
                FechaDesde = entity.FechaDesde,
                FechaHasta = entity.FechaHasta,
                MontoAlquiler = entity.MontoAlquiler,
                OrdenRenovacion = entity.OrdenRenovacion,
                Status = entity.Status,
                Domicilio = entity.Postulacion.Publicacion.Unidad.DomicilioCompleto,
                Archivo = entity.Archivo,
                InquilinosCount = entity.Firmas.Count(f => f.Rol == FirmaRol.Inquilino),
                PropietariosCount = entity.Firmas.Count(f => f.Rol == FirmaRol.Propietario),
                Inquilinos = string.Join(',', entity.Firmas.Where(f => f.Rol == FirmaRol.Inquilino).Select(f => f.Usuario.NombreCompleto)),
                Propietarios = string.Join(',', entity.Firmas.Where(f => f.Rol == FirmaRol.Propietario).Select(f => f.Usuario.NombreCompleto))
            };
    }
}
