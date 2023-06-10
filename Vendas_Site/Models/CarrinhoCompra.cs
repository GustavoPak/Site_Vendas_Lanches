using Microsoft.EntityFrameworkCore;
using Vendas_Site.Context;

namespace Vendas_Site.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinhoCompra(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<AppDbContext>();

            string CarrinhoId = session.GetString("CarrinhoId")??Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", CarrinhoId);

            return new CarrinhoCompra(context) { CarrinhoCompraId= CarrinhoId };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.carrinhoCompraItems.SingleOrDefault
                 (p => p.Lanche.LancheId == lanche.LancheId && p.CarrinhoCompraId == CarrinhoCompraId);

            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.carrinhoCompraItems.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }

        public void RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoItem = _context.carrinhoCompraItems.SingleOrDefault(p =>
            p.Lanche.LancheId == lanche.LancheId && p.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoItem != null)
            {
                if (carrinhoItem.Quantidade > 1)
                {
                    carrinhoItem.Quantidade--;
                }
                else
                {
                    _context.carrinhoCompraItems.Remove(carrinhoItem);
                }
            }

            _context.SaveChanges();
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
          return CarrinhoCompraItems ?? (CarrinhoCompraItems = 
                 _context.carrinhoCompraItems
                  .Where(p => p.CarrinhoCompraId == CarrinhoCompraId)
                   .Include(s => s.Lanche)
                     .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.carrinhoCompraItems
                                  .Where(p => p.CarrinhoCompraId == CarrinhoCompraId);

            _context.carrinhoCompraItems.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.carrinhoCompraItems.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total;
        }

    }
}
