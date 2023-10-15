namespace SGAA.Emails.EmailModels
{
    public class RechazarAplicacionEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
        public required IList<RechazarAplicacionComentarioEmailModel> Comentarios { get; set; }
    }

    public class RechazarAplicacionComentarioEmailModel
    {
        public required string Fecha { get; set; }
        public required string Comentario { get; set; }
    }
}
