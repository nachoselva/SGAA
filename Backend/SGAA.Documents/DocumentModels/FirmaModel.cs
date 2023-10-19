namespace SGAA.Documents.DocumentModels
{
    using System;

    public class FirmaModel
    {
        public required string NombreCompleto { get; set; }
        public required DateTime? FechaFirma { get; set; }
        public required string DireccionIp { get; set; }
        public required string Rol { get; set; }
        public required string TipoIdentificacion { get; set; }
        public required string NumeroIdentificacion { get; set; }
    }
}
