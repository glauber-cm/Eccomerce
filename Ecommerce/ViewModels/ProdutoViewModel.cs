using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
         
        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Estoque { get; set; }

        public string? ImagemUrl { get; set; }

        public IFormFile? ImagemUpload { get; set; }

        public Guid CategoriaId { get; set; }

        public IEnumerable<SelectListItem>? Categorias { get; set; }

    }
}
