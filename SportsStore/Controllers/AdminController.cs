using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index() => View(_repository.Products);
    }
}
