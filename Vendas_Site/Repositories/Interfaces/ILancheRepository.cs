using Vendas_Site.Models;

namespace Vendas_Site.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        public IEnumerable<Lanche> Lanches { get; }
        public IEnumerable<Lanche> LanchesPreferidos { get; }
        public Lanche GetLancheById(int id);
    }
}
