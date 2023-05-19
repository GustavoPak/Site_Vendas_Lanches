using Vendas_Site.Models;

namespace Vendas_Site.ViewModels
{
    public class LancheLIstViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }

    }
}
