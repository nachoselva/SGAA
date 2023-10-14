namespace SGAA.Emails.EmailModels
{
    public class CancelarPostulacionEmailModel : BaseEmailModel, IEmailModel
    {
        public required bool IsPropietarioAction { get; set; }
        public required string Domicilio { get; set; }
    }
}
