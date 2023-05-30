using Microsoft.AspNetCore.Mvc;
using Vendas_Site.Models;
using Vendas_Site.ViewModels;
using Vendas_Site.Repositories;
using Vendas_Site.Repositories.Interfaces;

namespace Vendas_Site.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        public readonly ILancheRepository _lancheRepository;
        public readonly CarrinhoCompra _CarrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _CarrinhoCompra = carrinhoCompra;
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var itens = _CarrinhoCompra.GetCarrinhoCompraItens();
            _CarrinhoCompra.CarrinhoCompraItems = itens;

            var CarrinhoVM = new CarrinhoCompraVIewModel
            {
                CarrinhoCompra = _CarrinhoCompra,
                CarrinhoCompraTotal = _CarrinhoCompra.CarrinhoCompraTotal()
            };

            return View(CarrinhoVM);
        }

        public IActionResult AdicionarCarrinho(int lancheId)
        {

            var lanche = _lancheRepository.Lanches
                                    .FirstOrDefault(p => p.LancheId == lancheId); ;
            if (lanche != null)
            {
                _CarrinhoCompra.AdicionarAoCarrinho(lanche);
            }


            return RedirectToAction("Index");
        }

        public IActionResult RemoverDoCarrinho(int lancheId)
        {
            var lanche = _lancheRepository.Lanches
                                    .FirstOrDefault(p => p.LancheId == lancheId); ;
            if (lanche != null)
            {
                _CarrinhoCompra.RemoverDoCarrinho(lanche);
            }

            return RedirectToAction("Index");
        }
    }
}
