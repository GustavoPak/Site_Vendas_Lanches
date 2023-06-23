using Microsoft.EntityFrameworkCore;
using Vendas_Site.Context;
using Vendas_Site.Models;

namespace Vendas_Site.Servicos
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime ?dateInit, DateTime ? dateFin)
        {
            var relatorio = _context.Pedidos.AsQueryable();

            if(dateInit != null)
            {
                relatorio = relatorio.Where(p => p.PedidoEnviado >= dateInit.Value);
            }
            if (dateFin != null)
            {
                relatorio = relatorio.Where(p => p.PedidoEnviado <= dateFin.Value);
            }

            return await relatorio.Include(p => p.PedidoItens)
                                  .ThenInclude(p => p.Lanche)
                                  .OrderByDescending(x => x.PedidoEnviado)
                                  .ToListAsync();
        }
    }
}
