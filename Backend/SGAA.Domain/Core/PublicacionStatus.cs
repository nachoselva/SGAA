namespace SGAA.Domain.Core
{
    public enum PublicacionStatus
    {
        Publicada,
        Cancelada,
        Ofrecida,
        Reservada
    }

    public static class PublicacionStatusExtensions
    {
        public static bool IsActive(this PublicacionStatus status)
        {
            return status == PublicacionStatus.Publicada || status == PublicacionStatus.Ofrecida;
        }
    }
}
