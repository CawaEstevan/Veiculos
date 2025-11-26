using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LojaCamisaGamer.Domain.Entities;

namespace LojaCamisaGamer.Infrastructure.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Ativa)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .IsRequired();

            // Configuração do relacionamento 1:N
            builder.HasMany(c => c.Camisas)
                .WithOne(ca => ca.Categoria)
                .HasForeignKey(ca => ca.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Nome);
        }
    }
}