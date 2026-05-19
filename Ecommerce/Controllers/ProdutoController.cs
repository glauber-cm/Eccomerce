using Ecommerce.Application.Service;
using Ecommerce.Web.ViewModels;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.AdicionarAsync(
                model.Nome,
                model.Descricao,
                model.Preco,
                model.Estoque);

            return RedirectToAction(nameof(Index));
        }
    }
}
