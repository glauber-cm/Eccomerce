using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Service
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CarrinhoService(IProdutoRepository produtoRepository, ICarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarProdutoAsync(Guid carrinhoId, Guid produtoId, int quantidade)
        {
            var carrinho = await _carrinhoRepository.ObterPorIdAsync(carrinhoId);

            if (carrinho == null)
                throw new Exception("Carrinho não encontrado!");

            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

            if (produto == null)
                throw new Exception("Produto não encontrado!");

            var item = new ItemCarrinho(carrinho.Id,produto.Id, quantidade, produto.Preco);

            //carrinho.AdicionarItem(item);

            await _carrinhoRepository.AdicionarItemAsync(item);

        }

        public async Task<Guid> CriarCarrinhoAsync()
        {
            var carrinho = new Carrinho();

            await _carrinhoRepository.AdicionarAsync(carrinho);

            return carrinho.Id;
        }

        public async Task<Carrinho?> ObterCarrinhoAsync(Guid carrinhoId)
        {
            return await _carrinhoRepository.ObterPorIdAsync(carrinhoId);
        }
    }
}
