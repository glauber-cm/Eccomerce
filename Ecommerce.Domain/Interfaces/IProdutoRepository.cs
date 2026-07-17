using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(Guid id);
        Task<(IReadOnlyList<Produto> Itens, int TotalItens)> BuscarLojaAsync(string? busca, Guid? categoriaId, string? ordenacao, int pagina, int tamanhoPagina);
    }
}
