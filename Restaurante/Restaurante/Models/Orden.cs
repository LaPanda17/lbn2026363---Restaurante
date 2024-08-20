using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
 
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<OrdenProducto> OrdenProductos { get; set; } // Relación de uno a muchos con OrdenProducto
        public Recibo Recibo { get; set; } // Relación de uno a uno con Recibo
    }
}