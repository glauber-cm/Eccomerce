using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        public async Task<Produto?> ObterPorIdAsync(Guid id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos
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
