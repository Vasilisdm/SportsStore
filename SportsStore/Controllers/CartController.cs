using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        
        public CartController(IProductRepository repo)
        {
            _repository = repo;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private void SaveCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        private Cart GetCart()
        {
            throw new NotImplementedException();
        }
    }
}
