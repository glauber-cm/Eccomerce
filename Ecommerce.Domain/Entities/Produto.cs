using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

        protected Produto() { }

        public Produto(Guid id, string nome, string descricao, decimal preco, int estoque, DateTime dataCadastro)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Estoque = estoque;
            DataCadastro = dataCadastro;
        }
    }
}
