using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

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
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] productArray = result.Products.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("P4", productArray[0].Name);
            Assert.Equal("P5", productArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "Iphone3"},
                new Product {ProductID = 2, Name = "Iphone4"},
                new Product {ProductID = 3, Name = "Iphone5"},
                new Product {ProductID = 4, Name = "Iphone6"},
                new Product {ProductID = 5, Name = "IphoneSe"},
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object) {PageSize = 3};

            // Act
            ProductsListViewModel result = controller.List(null,2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1, Category="Cat1", Name="Macbook pro mid 2014"},
                new Product {ProductID=2, Category="Cat2", Name="Macbook pro mid 2015"},
                new Product {ProductID=3, Category="Cat2", Name="Macbook pro mid 2016"},
                new Product {ProductID=4, Category="Cat2", Name="Macbook pro mid 2017"},
                new Product {ProductID=5, Category="Cat1", Name="Macbook pro mid 2018"},
                new Product {ProductID=6, Category="Cat1", Name="Macbook pro mid 2019"},
            }.AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object) {PageSize = 3};

            // Act
            Product[] result = (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();

            // Arrange
            Assert.Equal(3, result.Length);
            Assert.True(result[1].Name == "Macbook pro mid 2016" && result[1].Category == "Cat2");
            Assert.True(result[2].Name == "Macbook pro mid 2017" && result[2].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());

            ProductController target = new ProductController(mock.Object);

            Func<ViewResult, ProductsListViewModel> GetModel = result => result?.ViewData?.Model as ProductsListViewModel;

            // Action
            int? result1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? result2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? result3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resultAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(2, result1);
            Assert.Equal(2, result2);
            Assert.Equal(1, result3);
            Assert.Equal(5, resultAll);
        }
    }
}
