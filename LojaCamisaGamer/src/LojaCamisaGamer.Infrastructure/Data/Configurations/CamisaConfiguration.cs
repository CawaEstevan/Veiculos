using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LojaCamisaGamer.Domain.Entities;

namespace LojaCamisaGamer.Infrastructure.Data.Configurations
{
    public class CamisaConfiguration : IEntityTypeConfiguration<Camisa>
    {
        public void Configure(EntityTypeBuilder<Camisa> builder)
        {
            builder.ToTable("Camisas");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(c => c.Preco)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Estoque)
                .IsRequired();

            builder.Property(c => c.Tamanho)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(c => c.Cor)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.ImagemUrl)
                .HasMaxLength(500);

            builder.Property(c => c.Disponivel)
                .IsRequired();

            builder.Property(c => c.DataCadastro)
                .IsRequired();

            // Chave estrangeira explícita
            builder.Property(c => c.CategoriaId)
                .IsRequired();

            // Configuração do relacionamento N:1
            builder.HasOne(c => c.Categoria)
                .WithMany(cat => cat.Camisas)
                .HasForeignKey(c => c.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Nome);
            builder.HasIndex(c => c.CategoriaId);
        }
    }
}