using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class FakeProductRepository //: IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product {Name = "Macbook Pro mid 2014", Price = 1438},
            new Product {Name = "Macbook Pro mid 2015", Price = 1638},
            new Product {Name = "Macbook Pro mid 2016", Price = 1838}
        }.AsQueryable();
    }
}
