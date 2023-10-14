namespace SGAA.Emails.EmailModels
{
    public class PublicarUnidadEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
        public required string PublicacionURL { get; set; }
        public required string InicioAlquiler { get; set; }
        public required decimal MontoAlquiler { get; set; }
    }
}
