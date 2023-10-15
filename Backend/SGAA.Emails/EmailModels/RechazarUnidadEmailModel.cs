namespace SGAA.Emails.EmailModels
{
    public class RechazarUnidadEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
        public required IList<RechazarUnidadComentarioEmailModel> Comentarios { get; set; }
    }

    public class RechazarUnidadComentarioEmailModel
    {
        public required string Fecha { get; set; }
        public required string Comentario { get; set; }
    }
}
