using Ecommerce.Application.Service;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {

        private readonly ProdutoService _produtoService;

        public HomeController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task <IActionResult> Index()
        {
            var produtos = await _produtoService.ObterTodosAsync();

            return View(produtos);
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
