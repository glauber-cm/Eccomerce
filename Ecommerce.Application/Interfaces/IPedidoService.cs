using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<Guid> FinalizarPedidosAsync(Guid carrinhoId);
        Task<IEnumerable<Pedido>> ObterTodosAsync();
        Task<Pedido?> ObterPorIdAsync(Guid id);
    }
}
