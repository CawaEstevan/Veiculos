using Microsoft.EntityFrameworkCore;
using LojaCamisaGamer.Domain.Entities;
using LojaCamisaGamer.Infrastructure.Data.Configurations;

namespace LojaCamisaGamer.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Camisa> Camisas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new CamisaConfiguration());
        }
    }
}