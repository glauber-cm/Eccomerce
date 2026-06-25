using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly EcommerceDbContext _context;

        public CarrinhoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Carrinho carrinho)
        {
            await _context.Carrinhos.AddAsync(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaAsync(Carrinho carrinho)
        {
           // _context.Carrinhos.Update(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarItemAsync(ItemCarrinho item)
        {
            await _context.ItensCarrinho.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Carrinho?> ObterPorIdAsync(Guid id)
        {
           return await _context.Carrinhos
                .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task RemoverItemAsync(Guid itemId)
        {
            var item = await _context.ItensCarrinho.FirstOrDefaultAsync(c => c.Id == itemId);

            if (item is null)
                return;

            _context.ItensCarrinho.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}
