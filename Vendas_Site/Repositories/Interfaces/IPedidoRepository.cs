using Vendas_Site.Models;

namespace Vendas_Site.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        public void CriarPedido(Pedido pedido);
        public Pedido FindbyId(int id);

        public IEnumerable<Pedido> GetAll();
    }
}
