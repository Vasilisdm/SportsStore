using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public ViewResult List() => View(_repositoy.Orders.Where(o => !o.Shipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = _repositoy.Orders.FirstOrDefault(o => o.OrderId == orderID);

            if (order!=null)
            {
                order.Shipped = true;
                _repositoy.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        public ViewResult CheckOut() => View(new Order());

        [HttpPost]
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
