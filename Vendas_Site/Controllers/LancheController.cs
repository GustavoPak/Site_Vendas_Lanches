using Microsoft.AspNetCore.Mvc;
using Vendas_Site.Repositories.Interfaces;
using Vendas_Site.ViewModels;

namespace Vendas_Site.Controllers
{
    public class LancheController : Controller
    {
        public readonly ILancheRepository _IlancheRep;
        public readonly LancheLIstViewModel _ListViewModel;

        public LancheController(ILancheRepository ilancheRep)
        {
            _IlancheRep = ilancheRep;
        }

        public IActionResult List()
        {
            //var lanches = _IlancheRep.Lanches;
            //return View(_ListViewModel);

            var lanchelistVM = new LancheLIstViewModel();
            lanchelistVM.Lanches = _IlancheRep.Lanches;
            lanchelistVM.CategoriaAtual = "Categoria Atual";
            return View(lanchelistVM);
        }
    }
}
