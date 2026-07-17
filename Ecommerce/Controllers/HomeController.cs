using Ecommerce.Application.Service;
using Ecommerce.Models;
using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private readonly ProdutoService _produtoService;
        private readonly CategoriaService _categoriaService;


        public HomeController(ProdutoService produtoService, CategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.ObterTodosAsync();
            var categorias = await _categoriaService.ObterTodosAsync();

            var model = new HomeIndexViewModel
            {
                ProdutosRecentes = produtos
                       .Where(p => p.Estoque > 0)
                       .OrderByDescending(p => p.DataCadastro)
                       .Take(8)
                       .ToList(),

                Categorias = categorias
                        .OrderBy(c => c.Nome)
                        .Take(6)
                        .ToList()
            };


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
