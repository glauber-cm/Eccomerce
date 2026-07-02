using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Models
{
    public class AlterarPerfilViewModel
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PerfilSelecionado { get; set; } = string.Empty;
        public IEnumerable<SelectListItem> Perfis { get; set; } = new List<SelectListItem>();

    }
}
