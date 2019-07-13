using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repositoy;
        private Cart _cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repositoy = repoService;
            _cart = cartService;
        }

        public ViewResult CheckOut() => View(new Order());
    }
}
