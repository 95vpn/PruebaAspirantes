using System.ComponentModel.DataAnnotations;

namespace PruebaAspirantes.DTOs
{
    public class LogoutDto
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string? Token { get; set; }
    }
}
