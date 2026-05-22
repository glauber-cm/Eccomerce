using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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

    }
}
