using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaAspirantes.Models
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSession { get; set; }
        public string? FechaIngreso { get; set; }
        public string? FechaCierre { get; set; }

        public int IdUsuario { get; set; }

        public string? Token {  get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; } 
    }
}
