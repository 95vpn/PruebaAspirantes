using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaAspirantes.Models
{
    public class RolRolOpcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }
        public int IdOption { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; } 

        [ForeignKey("IdOption")]
        public virtual RolOpcion? RolOpcion { get; set; } 
    }
}
