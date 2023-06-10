using Vendas_Site.Context;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;

namespace Vendas_Site.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach(var item in _carrinhoCompra.CarrinhoCompraItems)
            {
                var NewPedido = new PedidoDetalhe
                {
                    LancheId = item.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Quantidade = item.Quantidade,
                    Preco = item.Lanche.Preco
                };

                _context.PedidoDetalhes.Add(NewPedido);
            }
            _context.SaveChanges();
        }

        public Pedido FindbyId(int id)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.PedidoId == id);

            return pedido;
        }

        public IEnumerable<Pedido> GetAll()
        {
            return _context.Pedidos;
        }
    }
}
