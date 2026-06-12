using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Mappings
{
    public class CarrinhoConfiguration : IEntityTypeConfiguration<Carrinho>
    {
        public void Configure(EntityTypeBuilder<Carrinho> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(c => c.Itens)
                    .WithOne(i => i.Carrinho)
                    .HasForeignKey(i => i.CarrinhoId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
