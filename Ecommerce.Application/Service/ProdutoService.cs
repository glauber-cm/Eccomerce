using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Service
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(Guid id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(string nome, string descricao, decimal preco, int estoque)
        {
            var produto = new Produto(nome, descricao, preco, estoque);

            await _repository.AdicionarAsync(produto); 
        }

        public async Task AtualizarAsync(Produto produto)
        {
            await _repository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _repository.RemoverAsync(id);
        }
    }
}
