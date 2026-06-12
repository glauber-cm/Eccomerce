using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICarrinhoService
    {
        Task<Guid> CriarCarrinhoAsync();
        Task AdicionarProdutoAsync(Guid carrinhoId, Guid produtoId, int quantidade);
        Task<Carrinho?> ObterCarrinhoAsync(Guid carrinhoId);
    }
}
