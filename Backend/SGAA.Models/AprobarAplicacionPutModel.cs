namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class AprobarAplicacionPutModel : IPutModel<Aplicacion>
    {
        public ICollection<PostulanteCalificacionModel> Puntuaciones { get; set; } = default!;
    }

    public class PostulanteCalificacionModel
    {
        public int PostulanteId { get; set; }
        public int PuntuacionCrediticia { get; set; }
        public int PuntuacionPenal { get; set; }
    }
}
