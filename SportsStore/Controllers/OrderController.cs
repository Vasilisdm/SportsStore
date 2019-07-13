using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Models;
using System;
using System.Linq;

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

        public ActionResult Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                _repositoy.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            } else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}
