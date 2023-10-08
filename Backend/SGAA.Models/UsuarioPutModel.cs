namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UsuarioPutModel
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
    }
}
