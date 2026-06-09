using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Carrinho
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public ICollection<ItemCarrinho> Itens { get; private set; }
        //protected Carrinho() { }
        public Carrinho()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            Itens = new List<ItemCarrinho>();
        }
    }
}
