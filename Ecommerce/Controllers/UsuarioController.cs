using Ecommerce.Infrastructure.Identity;
using Ecommerce.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
                Perfis = _roleManager.Roles
                            .Select(r => new SelectListItem
                            {
                                Value = r.Name,
                                Text = r.Name
                            })
                            .ToList()
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

        [HttpGet]
        public async Task<IActionResult> MeuPerfil()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
                return RedirectToAction("Login", "Account");

            var model = new MeuPerfilViewModel
            {
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email ?? string.Empty,
                Telefone = usuario.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MeuPerfil(MeuPerfilViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _userManager.GetUserAsync(User);

            if (usuario == null)
                return RedirectToAction("Login", "Account");

            usuario.NomeCompleto = model.NomeCompleto;
            usuario.Email = model.Email;
            usuario.UserName = model.Email;
            usuario.PhoneNumber = model.Telefone;

            var result = await _userManager.UpdateAsync(usuario);

            if (result.Succeeded)
            {
                TempData["Sucesso"] = "Perfil atualizado com sucesso.";
                return RedirectToAction(nameof(MeuPerfil));
            }

            foreach (var erro in result.Errors)
                ModelState.AddModelError("", erro.Description);

            return View(model);

        }
    }
}
