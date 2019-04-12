using DependencyInjection.Controllers;
using DependencyInjection.Infrastructure;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace DependencyInjection.Tests
{
    public class DITests
    {
        [Fact]
        public void ControllerTest()
        {
            // arrange
            var data = new[] { new Product { Name = "Test", Price = 100 } };
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);

            TypeBroker.SetTestObject(mock.Object);
            HomeController controller = new HomeController();

            //HomeController controller = new HomeController
            //{
            //    Repository = mock.Object
            //};

            // act
            ViewResult result = controller.Index();

            // assert
            Assert.Equal(data, result.ViewData.Model);
        }

        [Fact]
        public void ControllerDITest()
        {
            // arrange
            var data = new[] { new Product { Name = "Test", Price = 100 } };
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);

            DIController controller = new DIController(mock.Object, null);

            // act
            ViewResult result = controller.Index();

            // assert
            Assert.Equal(data, result.ViewData.Model);
        }
    }
}
