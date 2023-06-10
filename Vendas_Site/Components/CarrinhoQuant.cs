using Microsoft.AspNetCore.Mvc;
using Vendas_Site.Models;
using Vendas_Site.ViewModels;

namespace Vendas_Site.Components
{
    public class CarrinhoQuant : ViewComponent
    {
        private readonly CarrinhoCompra _Carrinho;
        

        public CarrinhoQuant(CarrinhoCompra carrinho)
        {
            _Carrinho = carrinho;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _Carrinho.GetCarrinhoCompraItens();
            _Carrinho.CarrinhoCompraItems = itens;

            var CarrinhoVM = new CarrinhoCompraVIewModel
            {
                CarrinhoCompra = _Carrinho,
                CarrinhoCompraTotal = _Carrinho.GetCarrinhoCompraTotal()
            };

            return View(CarrinhoVM);
        }
    }
}
