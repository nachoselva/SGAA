namespace SGAA.Emails.EmailModels
{
    public class RechazarAplicacionEmailModel : BaseEmailModel, IEmailModel
    {
        public required IList<ComentarioEmailModel> Comentarios { get; set; }
    }
}
