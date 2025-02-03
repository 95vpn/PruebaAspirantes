using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaAspirantes.Models
{
    public class RolUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdRol {  get; set; }

        public int IdUsuario { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol? Rol { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; }
    }
}
