using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.Models
{
    public class MeuPerfilViewModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }


    }
}
