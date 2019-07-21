using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable<Product>());

            AdminController cntrl = new AdminController(mock.Object);

            // Act
            Product[] result = GetViewModel<IEnumerable<Product>>(cntrl.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]{
                
            }.AsQueryable<Product>());

            AdminController cntrl = new AdminController(mock.Object);

            // Act
            Product p1 = GetViewModel<Product>(cntrl.Edit(1));
            Product p2 = GetViewModel<Product>(cntrl.Edit(2));
            Product p3 = GetViewModel<Product>(cntrl.Edit(3));

            // Assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            AdminController cntrl = new AdminController(mock.Object);

            // Act
            Product nonExistingProduct = GetViewModel<Product>(cntrl.Edit(4));

            // Assert
            Assert.Null(nonExistingProduct);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            AdminController cntrl = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            Product product = new Product { Name = "Test" };

            // Act
            IActionResult result = cntrl.Edit(product);

            // Assert
            // Check if the SaveProduct of the repository was called
            mock.Verify(m => m.SaveProduct(product));

            // Check if the result type is redirect action
            Assert.IsType<RedirectToActionResult>(result);

            // Check the name of the action method that the user is being redirected to.
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }
    }
}
