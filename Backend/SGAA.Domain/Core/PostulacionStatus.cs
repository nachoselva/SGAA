namespace SGAA.Domain.Core
{
    public enum PostulacionStatus
    {
        Postulada,
        Ofrecida,
        PublicacionCancelada,
        PostulacionCancelada,
        OfertaRechazada,
        Reservada
    }

    public static class PostulacionStatusExtensions
    {
        public static bool IsActive(this PostulacionStatus status)
        => status == PostulacionStatus.Postulada
            || status == PostulacionStatus.Ofrecida
            || status == PostulacionStatus.Reservada;
    }
}
