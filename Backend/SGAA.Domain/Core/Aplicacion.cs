namespace SGAA.Domain.Core
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;
    using System.Collections.Generic;

    public class Aplicacion : BaseEntity, IEntity
    {
        public List<Postulante> _postulantes;
        public List<Garantia> _garantias;
        public List<AplicacionComentario> _comentarios;

        public Aplicacion(int inquilinoUsuarioId, AplicacionStatus status, decimal puntuacionTotal)
        {
            InquilinoUsuarioId = inquilinoUsuarioId;
            Status = status;
            PuntuacionTotal = puntuacionTotal;
            _postulantes = new List<Postulante>();
            _garantias = new List<Garantia>();
            _comentarios = new List<AplicacionComentario>();
        }

        public int InquilinoUsuarioId { get; set; }
        public AplicacionStatus Status { get; set; }
        public decimal PuntuacionTotal { get; set; }

        public Usuario InquilinoUsuario { get; } = null!;
        public IReadOnlyCollection<Postulacion> Postulaciones { get; } = new List<Postulacion>();
        public IReadOnlyCollection<Postulante> Postulantes => _postulantes;
        public IReadOnlyCollection<Garantia> Garantias => _garantias;
        public IReadOnlyCollection<AplicacionComentario> Comentarios => _comentarios;

        public void AddGarantias(IEnumerable<Garantia> garantias)
        {
            _garantias.AddRange(garantias);
        }

        public void AddPostulantes(IEnumerable<Postulante> postulantes)
        {
            _postulantes.AddRange(postulantes);
        }
        public void AddComentario(AplicacionComentario comentario)
        {
            _comentarios.Add(comentario);
        }

        public void RemoveGarantias(IEnumerable<Garantia> entitiesToDelete)
        {
            IEnumerable<int> idsToDelete = entitiesToDelete.Select(gar => gar.Id);
            _garantias.RemoveAll(gar => idsToDelete.Contains(gar.Id));
        }

        public void RemovePostulantes(IEnumerable<Postulante> entitiesToDelete)
        {
            IEnumerable<int> idsToDelete = entitiesToDelete.Select(pos => pos.Id);
            _postulantes.RemoveAll(pos => idsToDelete.Contains(pos.Id));
        }
    }
}
