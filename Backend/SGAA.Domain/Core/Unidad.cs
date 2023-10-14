namespace SGAA.Domain.Core
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;

    public class Unidad : BaseEntity, IEntity
    {
        private List<UnidadComentario> _comentarios;
        private List<Titular> _titulares;

        public Unidad(int propiedadId, int propietarioUsuarioId, string piso, string departamento, DateTime fechaAdquisicion, byte[] tituloPropiedadArchivo, UnidadStatus status)
        {
            PropiedadId = propiedadId;
            PropietarioUsuarioId = propietarioUsuarioId;
            Piso = piso;
            Departamento = departamento;
            FechaAdquisicion = fechaAdquisicion;
            TituloPropiedadArchivo = tituloPropiedadArchivo;
            Status = status;
            _comentarios = new List<UnidadComentario>();
            _titulares = new List<Titular>();
        }

        public int PropiedadId { get; set; }
        public int PropietarioUsuarioId { get; private set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public byte[] TituloPropiedadArchivo { get; set; }
        public UnidadStatus Status { get; set; }

        public string DomicilioCompleto => $"{Propiedad.Calle} {Propiedad.Altura} {Piso} {Departamento}, {Propiedad.Ciudad.Nombre} - {Propiedad.Ciudad.Provincia.Nombre}";
        public Propiedad Propiedad { get; set; } = default!;
        public Usuario PropietarioUsuario { get; set; } = default!;
        public UnidadDetalle Detalle { get; set; } = default!;
        public IReadOnlyCollection<UnidadComentario> Comentarios => _comentarios;
        public IReadOnlyCollection<Publicacion> Publicaciones { get; private set; } = new List<Publicacion>();
        public IReadOnlyCollection<Titular> Titulares => _titulares;

        public void AddComentario(UnidadComentario comentario)
        {
            _comentarios.Add(comentario);
        }

        public void AddTitulares(IEnumerable<Titular> titulares)
        {
            _titulares.AddRange(titulares);
        }

        public void RemoveTitulares(Titular[] titulares)
        {
            IEnumerable<int> idsToDelete = titulares.Select(img => img.Id);
            _titulares.RemoveAll(img => idsToDelete.Contains(img.Id));
        }
    }
}