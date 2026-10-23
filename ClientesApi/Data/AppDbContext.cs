using ClientesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientesApi.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor (inicializar)
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }

        //ORM tendrá acceso al módelo cliente
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }

    }
}
