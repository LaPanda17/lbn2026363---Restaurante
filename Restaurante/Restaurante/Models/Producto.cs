using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public List<OrdenProducto> OrdenProductos { get; set; } // Relación de uno a muchos con OrdenProducto
    }
}
