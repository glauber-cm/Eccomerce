using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Models
{
    public class LojaIndexViewModel
    {
        public IReadOnlyList<Produto> Produtos { get; set; } = new List<Produto>();
        public IEnumerable<SelectListItem> Categorias { get; set; } = new List<SelectListItem>();
        public string? Busca { get; set; }
        public Guid? CategoriaId { get; set; }
        public string? Ordenacao { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
        public bool TemPaginaAnterior => PaginaAtual > 1; 
        public bool TemProximaPagina => PaginaAtual < TotalPaginas;
    }
}
