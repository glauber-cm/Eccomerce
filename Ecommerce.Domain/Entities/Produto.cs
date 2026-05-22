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
        public string? ImageUrl { get; set; }

        //protected Produto() { }

        public Produto(string nome, string descricao, decimal preco, int estoque, string? imageUrl)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Estoque = estoque;
            ImageUrl = imageUrl;
            DataCadastro = DateTime.Now.Date;
        }

        public void Atualizar(
            string nome,
            string descricao,
            decimal preco,
            int estoque,
            string? imageUrl)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Estoque = estoque;
            ImageUrl = imageUrl;
        }
    }
}
