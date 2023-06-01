using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;
using Vendas_Site.ViewModels;

namespace Vendas_Site.Controllers
{
    public class LancheController : Controller
    {
        public readonly ILancheRepository _IlancheRep;
        public readonly ICategoriaRepository _IcategoriaRep;
        public readonly LancheLIstViewModel _ListViewModel;

        public LancheController(ILancheRepository ilancheRep, ICategoriaRepository icategoriaRep)
        {
            _IlancheRep = ilancheRep;
            _IcategoriaRep = icategoriaRep;
        }

        public IActionResult List(string categoria)
        {
            //var lanches = _IlancheRep.Lanches;
            //return View(_ListViewModel);

            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            Categoria categoriaAt = _IcategoriaRep
                             .Categorias.FirstOrDefault(p => p.CategoriaNome == categoria);
            
            if (categoriaAt == null)
            {
                categoriaAtual = "Todos os lanches";
                lanches = _IlancheRep.Lanches;

                var LancheVM = new LancheLIstViewModel
                {
                    Lanches = lanches,
                    CategoriaAtual = categoriaAtual
                };

                return View(LancheVM);
            }
            else
            {
                categoriaAtual = categoriaAt.CategoriaNome;
                lanches = _IlancheRep.Lanches.Where(p => p.Categoria == categoriaAt);

                var LancheVM = new LancheLIstViewModel
                {
                    Lanches = lanches,
                    CategoriaAtual = categoriaAtual
                };

                return View(LancheVM);
            }
             
        }
    }
}
