using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            // arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // act
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            // assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        private T GetViewModel<T>(IActionResult actionResult) where T : class
        {
            return (actionResult as ViewResult)?.ViewData.Model as T;
        }
        
        [Fact]
        public void Can_Edit_Product()
        {
            // arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            // arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // act
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            // assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            // arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            // arrange - create the controller
            AdminController target = new AdminController(mock.Object);

            // act
            Product result = GetViewModel<Product>(target.Edit(4));

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // arrange - create mock temp data
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            // arrange - create the controller
            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            // arrange - create a product
            Product product = new Product { Name = "Test" };

            // act - try to save the product
            IActionResult result = target.Edit(product);

            // assert - check that the repository was called
            mock.Verify(m => m.SaveProduct(product));
            // assert - check the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            // arrange - create mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // arrange - create the controller
            AdminController target = new AdminController(mock.Object);
            // arrange - create a product
            Product product = new Product { Name = "Test" };
            // arrange - add an error to the model state
            target.ModelState.AddModelError("error", "error");

            // act - try to save the product
            IActionResult result = target.Edit(product);

            // assert - check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // assert - check the method result type
            Assert.IsType<ViewResult>(result);
        }
    }
}
