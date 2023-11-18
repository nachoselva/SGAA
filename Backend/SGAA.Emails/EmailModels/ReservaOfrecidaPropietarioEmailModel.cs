namespace SGAA.Emails.EmailModels
{
    public class ReservaOfrecidaPropietarioEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }

}
