using Vendas_Site.Models;

namespace Vendas_Site.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        public IEnumerable<Categoria> Categorias { get; }
    }
}
