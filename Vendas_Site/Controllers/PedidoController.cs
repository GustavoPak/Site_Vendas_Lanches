using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Vendas_Site.Models;
using Vendas_Site.Repositories.Interfaces;

namespace Vendas_Site.Controllers
{
    public class PedidoController : Controller
    {
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(CarrinhoCompra carrinhoCompra, IPedidoRepository pedidoRepository)
        {
            _carrinhoCompra = carrinhoCompra;
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //Obter os itens do carrinho de compra

            List<CarrinhoCompraItem> CarrinhoItens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = CarrinhoItens;

            //Verificar se existem itens para fazer o pedido

            if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("","Não existem itens no seu carrinho,Que tal alguns lanches?");
            }

            //Calcular total de itens e valor total do pedido

            foreach(var item in CarrinhoItens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //Atribuir os valores obtidos ao pedido
            
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //Validar os dados do pedido
            if (ModelState.IsValid)
            {
                //Criar pedido! Ufa 
                _pedidoRepository.CriarPedido(pedido);

                //Deixar mensagem ao cliente
                ViewBag.CheckoutDoneMessage = "Seu Pedido está a caminho! :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //Limpar carrinho
                _carrinhoCompra.LimparCarrinho();

                //Exibir a view de conclusão do pedido

                return View("~/Views/Pedido/CheckoutCompleto.cshtml",pedido);

            }

            return View(pedido);
        }

        public IActionResult CheckoutCompleto()
        {
            return View();
        }
    }
}
