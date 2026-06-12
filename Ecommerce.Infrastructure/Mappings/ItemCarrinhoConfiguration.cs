using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Mappings
{
    public class ItemCarrinhoConfiguration : IEntityTypeConfiguration<ItemCarrinho>
    {
        public void Configure(EntityTypeBuilder<ItemCarrinho> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId);

            builder.HasOne(x => x.Carrinho)
                .WithMany(x => x.Itens)
                .HasForeignKey(x => x.CarrinhoId);
        }
    }
}
