namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class TitularModel : IGetModel<Titular>, IPostModel<Titular>, IPutModel<Titular>
    {
        public int? Id { get; set; }
        public int? UnidadId { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public required string Domicilio { get; set; }
        public required string FrenteIdentificacionArchivo { get; set; }
        public required string DorsoIdentificacionArchivo { get; set; }
    }
}
