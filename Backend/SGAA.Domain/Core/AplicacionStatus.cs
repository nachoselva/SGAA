namespace SGAA.Domain.Core
{
    public enum AplicacionStatus
    {
        AprobacionPendiente,
        Aprobada,
        Expirada,
        Ofrecida,
        Reservada
    }

    public static class AplicacionStatusExtensions
    {
        public static bool IsActive(this AplicacionStatus status)
        {
            return status == AplicacionStatus.AprobacionPendiente || status == AplicacionStatus.Aprobada || status == AplicacionStatus.Ofrecida;
        }
    }
}
