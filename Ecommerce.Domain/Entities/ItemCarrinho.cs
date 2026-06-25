using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class ItemCarrinho
    {
        public Guid Id { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Carrinho Carrinho { get; private set; }
        public Produto Produto { get; private set; }
        protected ItemCarrinho() { }

        public ItemCarrinho(Guid carrinhoId, Guid produtoId, int quantidade, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
            CarrinhoId = carrinhoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AumentarQuantidade()
        {
            Quantidade++;
        }

        public void DiminuirQuantidade()
        {
            if(Quantidade > 1) 
               Quantidade--;
        }
    }
}
