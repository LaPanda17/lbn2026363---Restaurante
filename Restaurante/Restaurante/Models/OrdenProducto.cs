using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class OrdenProducto
    {
        [Key]
        public int OrdenId { get; set; }
        public Orden Orden { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
