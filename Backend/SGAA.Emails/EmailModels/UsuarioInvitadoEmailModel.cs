namespace SGAA.Emails.EmailModels
{
    public class UsuarioInvitadoEmailModel : BaseEmailModel, IEmailModel
    {
        public required string InvitacionURL { get; set; }
    }
}
