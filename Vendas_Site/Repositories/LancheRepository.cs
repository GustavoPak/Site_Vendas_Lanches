using Microsoft.EntityFrameworkCore;
using Vendas_Site.Context;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;

namespace Vendas_Site.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        public readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p => p.IsLanchePreferido).Include(c => c.Categoria);

        public Lanche GetLancheById(int lancheid)
        {
            return _context.Lanches.FirstOrDefault(p => p.LancheId == lancheid);
        }
    }
}
