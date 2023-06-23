using Vendas_Site.Context;
using Vendas_Site.Models;

namespace Vendas_Site.Areas.Admin.Servicos
{
    public class GraficoVendasService
    {
        private readonly AppDbContext _context;

        public GraficoVendasService(AppDbContext context)
        {
            _context = context;
        }

        public List<LancheGrafico> GetVendasLanche(int dias = 360)
        {
            var data = DateTime.Now.AddDays(-dias);

            var lanches = (from PedidoDet in _context.PedidoDetalhes
                           join lanc in _context.Lanches
                            on PedidoDet.LancheId equals lanc.LancheId
                           where PedidoDet.Pedido.PedidoEnviado >= data
                           group PedidoDet
                             by new { PedidoDet.LancheId, lanc.Nome }
                                 into g
                           select new
                           {
                               lancheNome = g.Key.Nome,
                               lancheQuantidadeTotal = g.Sum(g => g.Quantidade),
                               lancheValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                           });

            var list = new List<LancheGrafico>();

            foreach (var item in lanches)
            {
                var lanche = new LancheGrafico
                {
                    LancheNome = item.lancheNome,
                    LanchesQuantidade = item.lancheQuantidadeTotal,
                    LanchesValorTotal = item.lancheValorTotal
                };

                list.Add(lanche);
            }

            return list;
        }
    }
}
