using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public List<Orden> Ordenes { get; set; }
    }
}
