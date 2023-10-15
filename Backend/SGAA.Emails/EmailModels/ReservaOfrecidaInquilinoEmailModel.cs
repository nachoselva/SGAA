namespace SGAA.Emails.EmailModels
{
    public class ReservaOfrecidaInquilinoEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }

}
