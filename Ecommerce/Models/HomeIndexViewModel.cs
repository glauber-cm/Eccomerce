using Ecommerce.Domain.Entities;

namespace Ecommerce.Web.Models
{
    public class HomeIndexViewModel
    {
        public IReadOnlyList<Produto> ProdutosRecentes { get; set; } = new List<Produto>();
        public IReadOnlyList<Categoria> Categorias { get; set; } = new List<Categoria>();
    }
}
