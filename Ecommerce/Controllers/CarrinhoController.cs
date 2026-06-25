using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        public async Task<IActionResult> Index(Guid carrinhoId)
        {
            var carrinho = await _carrinhoService.ObterCarrinhoAsync(carrinhoId);

            if (carrinho == null)
                return NotFound();

            return View(carrinho);
        }

        public async Task<IActionResult> Adicionar(Guid produtoId)
        {
            Guid carrinhoId;

            if (HttpContext.Session.GetString("CarrinhoId") == null)
            {
                carrinhoId = await _carrinhoService.CriarCarrinhoAsync();

                HttpContext.Session.SetString("CarrinhoId", carrinhoId.ToString());
            }
            else
            {
                carrinhoId = Guid.Parse(HttpContext.Session.GetString("CarrinhoId")!);
            }

            await _carrinhoService.AdicionarProdutoAsync(carrinhoId, produtoId, 1);

            return RedirectToAction("Index", new {carrinhoId});
        }

        public async Task<IActionResult> Remover(Guid itemId)
        {
            await _carrinhoService.RemoverItemAsync(itemId);

            var carrinhoId = HttpContext.Session.GetString("CarrinhoId");

            return RedirectToAction(nameof(Index), new {carrinhoId});
        }
    }
}