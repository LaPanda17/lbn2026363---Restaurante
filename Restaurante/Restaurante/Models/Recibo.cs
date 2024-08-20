using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class Recibo
    {
        [Key]
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public Orden Orden { get; set; }
        public decimal Total { get; set; }
        public decimal Cambio { get; set; }
    }
}
