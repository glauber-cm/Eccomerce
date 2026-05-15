using Ecommerce.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _service.ObterTodosAsync();

            return View(produtos);
        }
    }
}
