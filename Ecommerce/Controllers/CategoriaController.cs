using AutoMapper;
using Ecommerce.Application.Service;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriaController : Controller
    {
        private readonly CategoriaService _service;
        private readonly IMapper _mapper;

        public CategoriaController(CategoriaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _service.ObterTodosAsync();

            var model = _mapper.Map<IEnumerable<CategoriaViewModel>>(categorias);


            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            await _service.AdicionarAsync(model.Nome);

            return RedirectToAction(nameof(Index));
        }

    }
}
