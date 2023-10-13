namespace SGAA.Domain.Core
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;

    public class Unidad : BaseEntity, IEntity
    {
        public Unidad(int propiedadId, int propietarioUsuarioId, string piso, string departamento, DateTime fechaAdquisicion, byte[] tituloPropiedadArchivo, UnidadStatus status)
        {
            PropiedadId = propiedadId;
            PropietarioUsuarioId = propietarioUsuarioId;
            Piso = piso;
            Departamento = departamento;
            FechaAdquisicion = fechaAdquisicion;
            TituloPropiedadArchivo = tituloPropiedadArchivo;
            Status = status;
        }

        public int PropiedadId { get; set; }
        public int PropietarioUsuarioId { get; private set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public byte[] TituloPropiedadArchivo { get; set; }
        public UnidadStatus Status { get; private set; }

        public Propiedad Propiedad { get; set; } = default!;
        public Usuario PropietarioUsuario { get; set; } = default!;
        public UnidadDetalle Detalle { get; set; } = default!;
        public IReadOnlyCollection<UnidadComentario> Comentarios { get; private set; } = new List<UnidadComentario>();
        public IReadOnlyCollection<Publicacion> Publicaciones { get; private set; } = new List<Publicacion>();
        public IReadOnlyCollection<Titular> Titulares { get; private set; } = new List<Titular>();
    }
}