using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaAspirantes.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? SessionActive { get; set; }
        public string? Status { get; set; }
        public int IdPersona { get; set; }  

        [ForeignKey("IdPersona")]
        public virtual Persona? Persona { get; set; }
    }
}
