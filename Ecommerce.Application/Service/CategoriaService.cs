using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Service
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Categoria>> ObterTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Categoria?> ObterTodosPorId(Guid id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(string nome)
        {
            var categoria = new Categoria(nome);

            await _repository.AdicionarAsync(categoria);
        }
    }
}
