using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ADIELCARDOSO\ESTUDOS;Database=EFCore;User Id=sa;Password=1234;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }


    }
}