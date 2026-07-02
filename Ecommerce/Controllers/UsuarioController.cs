using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = _userManager.Users.ToList();

            var model = new List<UsuarioViewModel>();

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);

                model.Add(new UsuarioViewModel
                {
                    Id = usuario.Id,
                    Email = usuario.Email ?? "",
                    Perfil = roles.FirstOrDefault() ?? "Sem perfil"
                });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AlterarPerfil(string usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);

            if (usuario == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(usuario);

            var perfis = new[] { "Admin", "Gerente", "Vendedor", "Cliente" };

            var model = new AlterarPerfilViewModel
            {
                UsuarioId = usuario.Id,
                Email = usuario.Email ?? "",
                PerfilSelecionado = roles.FirstOrDefault() ?? "",
                Perfis = perfis.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p,
                    Text = p
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AlterarPerfil(AlterarPerfilViewModel model)
        {
            var usuario = await _userManager.FindByIdAsync(model.UsuarioId);

            if (usuario == null)
                return NotFound();

            if (!await _roleManager.RoleExistsAsync(model.PerfilSelecionado))
                await _roleManager.CreateAsync(new IdentityRole(model.PerfilSelecionado));

            var rolesAtuais = await _userManager.GetRolesAsync(usuario);

            if (rolesAtuais.Any())
                await _userManager.RemoveFromRolesAsync(usuario, rolesAtuais);

            await _userManager.AddToRoleAsync(usuario, model.PerfilSelecionado);

            return RedirectToAction(nameof(Index));
        }
    }
}
