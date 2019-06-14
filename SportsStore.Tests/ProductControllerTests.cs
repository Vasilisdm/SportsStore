using Moq;
using Xunit;
using SportsStore.Models;
using SportsStore.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);

            // Act
            IEnumerable<Product> result = controller.List(2).ViewData.Model as IEnumerable<Product>;
        }
    }
}
