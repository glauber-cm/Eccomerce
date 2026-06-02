using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly EcommerceDbContext _context;

        public CategoriaRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria?> ObterPorIdAsync(Guid id)
        {
           return await _context.Categorias.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<IEnumerable<Categoria>> ObterTodosAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var categoria = await ObterPorIdAsync(id);

            if (categoria is null)
                return;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
