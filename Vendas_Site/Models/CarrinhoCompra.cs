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
        public CarrinhoCompraItem CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinhoCompra(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = service.GetService<AppDbContext>();

            string CarrinhoId = session.GetString("CarrinhoId")??Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", CarrinhoId);

            return new CarrinhoCompra(context) { CarrinhoCompraId= CarrinhoId };
        }
    }
}
