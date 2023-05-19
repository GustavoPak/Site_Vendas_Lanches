using Vendas_Site.Context;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;

namespace Vendas_Site.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public AppDbContext _context { get; set; }

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;

    }
}
