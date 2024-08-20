using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } // "Administrador" o "Cliente"
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }
}
