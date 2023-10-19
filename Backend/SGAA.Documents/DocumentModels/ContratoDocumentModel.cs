namespace SGAA.Documents.DocumentModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ContratoDocumentModel
    {
        public required string DomicilioCompleto { get; set; }
        public required string Ciudad { get; set; }
        public required string Provincia { get; set; }
        public required string FechaDesde { get; set; }
        public required string FechaHasta { get; set; }
        public required string FechaOferta { get; set; }
        public required decimal MontoAlquiler { get; set; }
        public required string? FirmadoFecha { get; set; }

        public required IReadOnlyCollection<FirmaContratoDocumentModel> FirmasPropietarios { get; set; }
        public required IReadOnlyCollection<FirmaContratoDocumentModel> FirmasInquilinos { get; set; }
    }

    public class FirmaContratoDocumentModel
    {
        public required string Rol { get; set; }
        public required string NombreCompleto { get; set; }
        public required string Domicilio { get; set; }
        public required string TipoIdentificacion { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required string? FechaFirma { get; set; }
        public required string? DireccionIP { get; set; }
    }
}
