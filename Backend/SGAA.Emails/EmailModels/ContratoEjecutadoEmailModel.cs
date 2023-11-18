namespace SGAA.Emails.EmailModels
{
    public class ContratoEjecutadoEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }

}
