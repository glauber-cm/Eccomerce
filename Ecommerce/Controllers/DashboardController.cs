using Ecommerce.Application.ViewModels;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EcommerceDbContext _context;

        public DashboardController(EcommerceDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalProdutos = await _context.Produtos.CountAsync(),
                TotalCategorias = await _context.Categorias.CountAsync(),
                TotalPedidos = await _context.Pedidos.CountAsync(),
                FaturamentoTotal = await _context.Pedidos.SumAsync(p => p.Total)
            };

            return View(model);

        }
    }
}
