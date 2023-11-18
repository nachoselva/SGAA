namespace SGAA.Emails.EmailModels
{
    public interface IEmailModel
    {
        public string Nombre { get; }
        public string Apellido { get; }
        public string NombreCompleto { get; }
    }
}
