namespace SGAA.Emails.EmailModels
{
    public class RechazarUnidadEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
        public required IList<ComentarioEmailModel> Comentarios { get; set; }
    }
}
