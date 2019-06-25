using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repo = _repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_repository.Products
                                   .Select(x => x.Category)
                                   .Distinct()
                                   .OrderBy(x => x));
        }
    }
}
