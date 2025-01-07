using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaAspirantes.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPersona { get; set; }
        public string? Names { get; set; }
        public string? LastNames { get; set; }
        public string? Identificacion { get; set; }
        public string? FechaNacimiento { get; set; }
    }
}
