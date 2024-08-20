using Microsoft.EntityFrameworkCore;

namespace Restaurante.Models
{
    public class RestauranteContext:DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options)
           : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenProducto> OrdenProductos { get; set; }
        public DbSet<Recibo> Recibos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }

    }
}
