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

        public int PropiedadId { get; private set; }
        public int PropietarioUsuarioId { get; private set; }
        public string Piso { get; private set; }
        public string Departamento { get; private set; }
        public DateTime FechaAdquisicion { get; private set; }
        public byte[] TituloPropiedadArchivo { get; private set; }
        public UnidadStatus Status { get; private set; }

        public Propiedad Propiedad { get; private set; } = default!;
        public Usuario PropietarioUsuario { get; private set; } = default!;
        public IReadOnlyCollection<UnidadComentario> Comentarios { get; private set; } = Array.Empty<UnidadComentario>();
        public IReadOnlyCollection<Publicacion> Publicaciones { get; private set; } = Array.Empty<Publicacion>();
        public IReadOnlyCollection<Titular> Titulares { get; private set; } = Array.Empty<Titular>();
    }
}