using Ecommerce.Application.ViewModels;

namespace Ecommerce.Application.Interfaces
{
    public interface ICarrinhoService
    {
        Task<Guid> CriarCarrinhoAsync();
        Task AdicionarProdutoAsync(Guid carrinhoId, Guid produtoId, int quantidade);
        Task<CarrinhoViewModel?> ObterCarrinhoAsync(Guid carrinhoId);
        Task RemoverItemAsync(Guid itemId);

    }
}
