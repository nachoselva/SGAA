namespace SGAA.Models
{
    public class ContratoPostModel
    {
        public required int PostulacionId { get; set; }
        public required DateTime FechaDesde { get; set; }
        public required DateTime FechaHasta { get; set; }
    }
}
