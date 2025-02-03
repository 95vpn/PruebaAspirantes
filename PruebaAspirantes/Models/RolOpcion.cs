using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaAspirantes.Models
{
    public class RolOpcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdOpcion { get; set; }

        public string? NameOption { get; set; }

        public virtual ICollection<RolRolOpcion>? RolRolOpciones { get; set; }
    }
}
