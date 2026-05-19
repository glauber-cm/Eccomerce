using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.ViewModels
{
    public class ProdutoViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe o estoque")]
        public int Estoque { get; set; }

    }
}
