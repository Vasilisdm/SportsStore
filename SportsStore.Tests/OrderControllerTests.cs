using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            Cart cart = new Cart();

            Order order = new Order();

            OrderController orderCntlr = new OrderController(mock.Object, cart);

            // Act
            ViewResult result = orderCntlr.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            // Assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }


        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Assert
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController orderCntrl = new OrderController(mock.Object, cart);

            orderCntrl.ModelState.AddModelError("error", "error");

            // Act - Try to check out
            ViewResult result = orderCntrl.Checkout(new Order()) as ViewResult;

            // Assert
            // Check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            // Check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            // Check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }


        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {

        }
    }
}
