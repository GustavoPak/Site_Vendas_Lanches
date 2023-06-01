using Microsoft.AspNetCore.Mvc;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;

namespace Vendas_Site.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private ICategoriaRepository _IcatRep;

        public CategoriaMenu(ICategoriaRepository icatRep)
        {
            _IcatRep = icatRep;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<Categoria> categorias = _IcatRep.Categorias;

            return View(categorias);
        }
    }
}
