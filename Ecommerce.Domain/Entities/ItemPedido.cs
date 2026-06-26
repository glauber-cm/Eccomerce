using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; } = string.Empty;
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal SubTotal { get; private set; }

        public Pedido Pedido { get; private set; }
        public Produto Produto { get; private set; }

        public ItemPedido
        (
            Guid produtoId,
            string produtoNome,
            int quantidade,
            decimal precoUnitario
        )
        {
            Id = Guid.NewGuid();
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            SubTotal = quantidade + precoUnitario;

        }
    }
}
