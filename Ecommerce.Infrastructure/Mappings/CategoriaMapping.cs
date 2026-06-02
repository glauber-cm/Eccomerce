using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.Produtos)
                .WithOne(x => x.Categoria)
                .HasForeignKey(x => x.CategoriaId);
        }
    }
}
