using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;
using Vendas_Site.ViewModels;

namespace Vendas_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _LancheRep;

        public HomeController(ILancheRepository lancheRep)
        {
            _LancheRep = lancheRep;
        }

        public IActionResult Index()
        {
            var LanchePref = _LancheRep.LanchesPreferidos;

            var lancheVM = new HomeViewModel
            {
                LanchesPreferidos = LanchePref
            };

            return View(lancheVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}