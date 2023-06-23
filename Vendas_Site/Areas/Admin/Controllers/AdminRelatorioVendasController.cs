using Microsoft.AspNetCore.Mvc;
using Vendas_Site.Servicos;

namespace Vendas_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasService relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService _relatorioVendasService)
        {
            relatorioVendasService = _relatorioVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? init,
            DateTime? fin)
        {
            if (!init.HasValue)
            {
                init = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!fin.HasValue)
            {
                fin = DateTime.Now;
            }

            ViewData["minDate"] = init.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = fin.Value.ToString("yyyy-MM-dd");

            var result = await relatorioVendasService.FindByDateAsync(init, fin);
            return View(result);
        }
    }
}
