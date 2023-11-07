namespace SGAA.Models
{
    public class RenovarContratoPostModel
    {
        public required DateOnly FechaHasta { get; set; }
        public required decimal MontoAlquiler { get; set; }
    }
}
