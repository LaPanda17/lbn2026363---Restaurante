using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class UsuarioRol
    {
        [Key]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
