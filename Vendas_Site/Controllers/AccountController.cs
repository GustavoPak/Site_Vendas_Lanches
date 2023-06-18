using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Vendas_Site.ViewModels;

namespace Vendas_Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel{
                URL= returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            
                var username = await _userManager.FindByNameAsync(loginVM.UserName);

            if (username != null)
            {
                var result = await _signInManager.PasswordSignInAsync(username, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                     return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Falha ao realizar login!");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel RegisterVM)
        {
            if (ModelState.IsValid)
            {
                var usuário = new IdentityUser() { UserName = RegisterVM.UserName, Email = RegisterVM.Email};
                var conclusão = await _userManager.CreateAsync(usuário, RegisterVM.Password);

                if (conclusão.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuário, "Member");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Falha ao realizar o registro");
                }
            }
            return View(RegisterVM);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
