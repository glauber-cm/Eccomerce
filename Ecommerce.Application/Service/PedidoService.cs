using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(ICarrinhoRepository carrinhoRepository, IPedidoRepository pedidoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Guid> FinalizarPedidosAsync(Guid carrinhoId)
        {
            var carrinho = await _carrinhoRepository.ObterPorIdAsync(carrinhoId);

            if (carrinho == null)
                throw new Exception("Carrinho não encontrado!");

            if (!carrinho.Itens.Any())
                throw new Exception("Carrinho vazio!");

            var pedido = new Pedido();

            foreach (var item in carrinho.Itens)
            {
                var itemPedido = new ItemPedido(
                    item.ProdutoId,
                    item.Produto.Nome,
                    item.Quantidade,
                    item.PrecoUnitario);

                pedido.AdicionarItem(itemPedido);
            }

            await _pedidoRepository.AdicionarAsync(pedido);

            return pedido.Id;
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid id)
        {
            return await _pedidoRepository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosAsync()
        {
           return await _pedidoRepository.ObterTodosAsync();
        }

        
    }
}
