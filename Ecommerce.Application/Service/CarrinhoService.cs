using Ecommerce.Application.ViewModels;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Application.Interfaces;

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

            var item = new ItemCarrinho(carrinho.Id, produto.Id, quantidade, produto.Preco);

            //carrinho.AdicionarItem(item);

            await _carrinhoRepository.AdicionarItemAsync(item);

        }

        public async Task AumentarQuantidadeAsync(Guid itemId)
        {
            var item = await _carrinhoRepository.ObterItemAsync(itemId);

            if (item == null)
                return;

            item.AumentarQuantidade();

            await _carrinhoRepository.SalvarAsync();
        }

        public async Task<Guid> CriarCarrinhoAsync()
        {
            var carrinho = new Carrinho();

            await _carrinhoRepository.AdicionarAsync(carrinho);

            return carrinho.Id;
        }

        public async Task DiminuirQuantidadeAsync(Guid itemId)
        {
            var item = await _carrinhoRepository.ObterItemAsync(itemId);

            if (item == null)
                return;

            item.DiminuirQuantidade();

            await _carrinhoRepository.SalvarAsync();
        }

        public async Task<CarrinhoViewModel?> ObterCarrinhoAsync(Guid carrinhoId)
        {
            var carrinho = await _carrinhoRepository.ObterPorIdAsync(carrinhoId);

            if (carrinho == null)
                return null;

            return new CarrinhoViewModel
            {
                Id = carrinho.Id,
                Itens = carrinho.Itens.Select(item => new ItemCarrinhoViewModel
                {
                    Id = item.Id,
                    ProdutoNome = item.Produto.Nome,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario
                }).ToList()
            };
        }

        public async Task RemoverItemAsync(Guid itemId)
        {
            await _carrinhoRepository.RemoverItemAsync(itemId);
         
        }
    }
}
