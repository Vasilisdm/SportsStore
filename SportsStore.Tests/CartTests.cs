using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartLine[] result = cart.Lines.ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 20);
            CartLine[] result = cart.Lines.OrderBy(p => p.Product.ProductID).ToArray();

            // Assert
            Assert.Equal(21, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p2, 1);

            // Act
            cart.RemoveLine(p2);

            // Assert 
            Assert.Equal(0, cart.Lines.Count(p => p.Product == p2));
            Assert.Equal(2, cart.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50 };

            Cart cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p1, 3);

            // Act
            decimal totalValue = cart.ComputeTotalValue();

            // Assert
            Assert.Equal(500, totalValue);
        }
    }
}
