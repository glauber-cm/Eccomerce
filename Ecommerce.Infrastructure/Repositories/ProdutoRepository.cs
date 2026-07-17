using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EcommerceDbContext _context;

        public ProdutoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);

            await _context.SaveChangesAsync();

        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);

            await _context.SaveChangesAsync();

        }

        public async Task<(IReadOnlyList<Produto> Itens, int TotalItens)> BuscarLojaAsync(string? busca, Guid? categoriaId, string? ordenacao, int pagina, int tamanhoPagina)
        {

            pagina = pagina < 1 ? 1 : pagina;
            tamanhoPagina = tamanhoPagina < 1 ? 12 : tamanhoPagina;

            var query = _context.Produtos
                .Include(p => p.Categoria)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                var termo = busca.Trim();

                query = query.Where(p => p.Nome.Contains(termo) || p.Descricao.Contains(termo));
            }

            if (categoriaId.HasValue && categoriaId.Value != Guid.Empty)
            {
                query = query.Where(p => p.CategoriaId == categoriaId.Value);
            }

            query = ordenacao switch
            {
                "menor-preco" => query.OrderBy(p => p.Preco),
                "maior-preco" => query.OrderByDescending(p => p.Preco),
                "nome" => query.OrderBy(p => p.Nome),
                "mais-recentes" => query.OrderByDescending(p => p.DataCadastro), _ => query.OrderBy(p => p.Nome)

            };

            var totalItens = await query.CountAsync();

            var deslocamento = Math.Max(0, (pagina - 1) * tamanhoPagina);

            var itens = await query
                .Skip(deslocamento)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (itens, totalItens);
        }

        public async Task<Produto?> ObterPorIdAsync(Guid id)
        {
            //return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            return await _context.Produtos
                .Include(p => p.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos
                        .Include(p => p.Categoria)
                        .AsNoTracking()
                        .ToListAsync() ?? new List<Produto>();
        }

        public async Task RemoverAsync(Guid id)
        {
            var produto = await ObterPorIdAsync(id);

            if (produto is null)
                return;

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }
    }
}
