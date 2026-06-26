using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<IActionResult> Finalizar()
        {
            var carrinhoIdSession = HttpContext.Session.GetString("CarrinhoId");

            if (string.IsNullOrEmpty(carrinhoIdSession))
                return RedirectToAction("Index", "Carrinho");

            var carrinhoId = Guid.Parse(carrinhoIdSession);

            var pedidoId = await _pedidoService.FinalizarPedidosAsync(carrinhoId);

            HttpContext.Session.Remove("CarrinhoId");

            return RedirectToAction(nameof(Sucesso), new { pedidoId });

        }

        public IActionResult Sucesso(Guid pedidoId)
        {
            ViewBag.PedidoId = pedidoId;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
