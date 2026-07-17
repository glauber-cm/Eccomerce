using Ecommerce.Application.Interfaces;
using Ecommerce.Application.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var carrinhoIdSession = HttpContext.Session.GetString("CarrinhoId");

            if (string.IsNullOrEmpty(carrinhoIdSession))
            {
                return View(new CarrinhoViewModel());
            }

            if (!Guid.TryParse(carrinhoIdSession, out var carrinhoId))
            {
                HttpContext.Session.Remove("CarrinhoId");

                return View(new CarrinhoViewModel());
            }

            var carrinho = await _carrinhoService.ObterCarrinhoAsync(carrinhoId);

            return View(carrinho ?? new CarrinhoViewModel());
        }

        public async Task<IActionResult> Adicionar(Guid produtoId)
        {
            if (produtoId == Guid.Empty)
            {
                return BadRequest("Produto inválido.");
            }

            Guid carrinhoId;

            var carrinhoIdSession = HttpContext.Session.GetString("CarrinhoId");


            if (string.IsNullOrWhiteSpace(carrinhoIdSession))
            {
                carrinhoId = await _carrinhoService.CriarCarrinhoAsync();
                HttpContext.Session.SetString("CarrinhoId", carrinhoId.ToString());
            }
            else
            {
                carrinhoId = Guid.Parse(carrinhoIdSession);
            }

            await _carrinhoService.AdicionarProdutoAsync(carrinhoId, produtoId, 1);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remover(Guid itemId)
        {
            await _carrinhoService.RemoverItemAsync(itemId);

            var carrinhoId = HttpContext.Session.GetString("CarrinhoId");

            return RedirectToAction(nameof(Index), new { carrinhoId });
        }

        public async Task<IActionResult> Aumentar(Guid itemId)
        {
            await _carrinhoService.AumentarQuantidadeAsync(itemId);

            var carrinhoId = HttpContext.Session.GetString("CarrinhoId");

            return RedirectToAction(nameof(Index), new { carrinhoId });
        }

        public async Task<IActionResult> Diminuir(Guid itemId)
        {
            await _carrinhoService.DiminuirQuantidadeAsync(itemId);

            var carrinhoId = HttpContext.Session.GetString("CarrinhoId");

            return RedirectToAction(nameof(Index), new { carrinhoId });
        }
    }
}