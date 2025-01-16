using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaAspirantes.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }
        public string? RolName { get; set; }

        public virtual ICollection<RolRolOpcion>? RolRolOpciones { get; set; }
    }
}
