using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public DateTime DataPedido { get; private set; }
        public decimal Total { get; private set; }
        public ICollection<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();

        public Pedido()
        {
            Id = Guid.NewGuid();
            DataPedido = DateTime.Now;
        }

        public void AdicionarItem(ItemPedido item)
        {
            Itens.Add(item);
            Total += item.SubTotal;
        }
    }
}
