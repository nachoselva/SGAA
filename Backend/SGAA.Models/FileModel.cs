namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class FileModel 
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Size { get; set; }
        public string? Base64 { get; set; }
    }
}
