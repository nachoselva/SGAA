namespace SGAA.Emails.EmailModels
{
    public abstract class BaseEmailModel
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
    }
}
