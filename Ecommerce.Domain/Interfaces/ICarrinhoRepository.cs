using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Carrinho carrinho);
        Task AtualizaAsync(Carrinho carrinho);
        Task AdicionarItemAsync(ItemCarrinho item);
        Task RemoverItemAsync(Guid itemId);
    }
}
