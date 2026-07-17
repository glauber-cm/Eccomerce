using Ecommerce.Application.Service;
using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class LojaController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly CategoriaService _categoriaService;


        public LojaController(ProdutoService produtoService, CategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? busca, Guid? categoriaId, string? ordenacao, int pagina = 1)
        {
            pagina = Math.Max(pagina, 1);

            const int tamanhoPagina = 12;

            var resultado = await _produtoService.BuscarLojaAsync(busca, categoriaId, ordenacao, pagina, tamanhoPagina);

            var categorias = await _categoriaService.ObterTodosAsync();

            var totalPaginas = (int)Math.Ceiling(resultado.TotalItens / (double)tamanhoPagina);

            var model = new LojaIndexViewModel
            {
                Produtos = resultado.Itens,
                Busca = busca,
                CategoriaId = categoriaId,
                Ordenacao = ordenacao,
                PaginaAtual = pagina,
                TotalItens = resultado.TotalItens,
                TotalPaginas = totalPaginas,
                Categorias = categorias.Select(c =>
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome,
                        Selected = categoriaId == c.Id
                    })
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
    }
}
