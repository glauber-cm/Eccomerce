using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Property(x => x.Preco)
                .HasColumnType("decimal(18,2)");
            
            builder.Property(x => x.Estoque)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .IsRequired();

        }
    }
}
