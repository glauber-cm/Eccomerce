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

        public async Task AdicionarAsync(string nome, string descricao, decimal preco, int estoque, string? imageUrl , Guid categoriaId)
        {
            var produto = new Produto(nome, descricao, preco, estoque, imageUrl, categoriaId);

            await _repository.AdicionarAsync(produto); 
        }

        public async Task AtualizarAsync(Guid id, string nome, string descricao, decimal preco, int estoque, string? imageUrl)
        {
            var produto = await _repository.ObterPorIdAsync(id);

            if (produto is null)
                return;

            produto.Atualizar(nome, descricao, preco, estoque, imageUrl);

            await _repository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _repository.RemoverAsync(id);
        }

        public async Task<(IReadOnlyList<Produto> Itens, int TotalItens)> BuscarLojaAsync(string? busca, Guid? categoriaId, string? ordenacao, int pagina, int tamanhoPagina)
        {
            pagina = Math.Max(pagina,1);

            tamanhoPagina = tamanhoPagina is < 1 or > 50 ? 12 : tamanhoPagina;

            return await _repository.BuscarLojaAsync(busca, categoriaId, ordenacao, pagina, tamanhoPagina);
        }
    }
}
