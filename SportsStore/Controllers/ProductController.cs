using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult List(int productPage = 1) => 
                                View(_repository.Products
                                                .OrderBy(p => p.ProductID)
                                                .Skip((productPage - 1) * PageSize)
                                                .Take(PageSize));
    }
}
