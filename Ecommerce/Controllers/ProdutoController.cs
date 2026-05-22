using Ecommerce.Application.Service;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace Ecommerce.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _enviroment;

        public ProdutoController(ProdutoService service, IMapper mapper, IWebHostEnvironment enviroment)
        {
            _service = service;
            _mapper = mapper;
            _enviroment = enviroment;
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

            string? nomeImagem = null;

            if (model.ImagemUpload != null)
            {
                nomeImagem = Guid.NewGuid() + Path.GetExtension(model.ImagemUpload.FileName);


                var caminho = Path.Combine(_enviroment.WebRootPath, "images", "produtos", nomeImagem);

                using var stream = new FileStream(caminho, FileMode.Create);

                await model.ImagemUpload.CopyToAsync(stream);
            }
            
            await _service.AdicionarAsync(
                model.Nome,
                model.Descricao,
                model.Preco,
                model.Estoque,
                nomeImagem);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await _service.ObterPorIdAsync(id);

            if (produto is null)
                return NotFound();

            var viewModel = _mapper.Map<ProdutoViewModel>(produto);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.AtualizarAsync(
                model.Id,
                model.Nome,
                model.Descricao,
                model.Preco,
                model.Estoque,
                model.ImagemUrl);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await _service.ObterPorIdAsync(id);

            if (produto is null)
                return NotFound();

            var viewModel = _mapper.Map<ProdutoViewModel>(produto);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProdutoViewModel model)
        {
            await _service.RemoverAsync(model.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await _service.ObterPorIdAsync(id);

            if (produto is null)
                return NotFound();

            var viewModel = _mapper.Map<ProdutoViewModel>(produto);

            return View(viewModel);
        }
    }
}
