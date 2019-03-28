using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;
using Moq;

namespace WorkingWithVisualStudio.Tests
{
    public class HomeControllerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            //public IEnumerable<Product> Products { get; } = new Product[]
            //{
            //    new Product { Name = "Kayak", Price = 275M },
            //    new Product { Name = "Lifejacket", Price = 48.95M },
            //    new Product { Name = "Soccer ball", Price = 19.50M },
            //    new Product { Name = "Corner flag", Price = 34.95M }
            //};
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }
        [Fact]
        public void IndexActionModelIsComplete()
        {
            // arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository();

            // act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // assert
            Assert.Equal(SimpleRepository.SharedRepository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        [Theory]
        [InlineData(275, 48.95, 19.50, 24.95)]
        [InlineData(5, 48.95, 19.50, 24.95)]
        public void IndexActionModelIsComplete2(decimal price1, decimal price2, decimal price3, decimal price4)
        {
            // arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = new Product[] {
                    new Product {Name = "P1", Price = price1 },
                    new Product {Name = "P2", Price = price2 },
                    new Product {Name = "P3", Price = price3 },
                    new Product {Name = "P4", Price = price4 },
                }
            };

            // act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete3(Product[] products)
        {
            // arrange
            //var controller = new HomeController();
            //controller.Repository = new ModelCompleteFakeRepository
            //{
            //    Products = products
            //};

            // arrange - using Moq
            var mock = new Mock<IRepository>();
            mock.Setup(m => m.Products).Returns(products);
            var controller = new HomeController { Repository = mock.Object };

            // act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; }
            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product { Name = "P1", Price = 100 } };
                }
            }

            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            // arrange
            //var repo = new PropertyOnceFakeRepository();
            //var controller = new HomeController { Repository = repo };

            // arrange - using Moq
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(new[] { new Product { Name = "P1", Price = 100 } });
            var controller = new HomeController { Repository = mock.Object };

            // act
            var result = controller.Index();

            // assert
            //Assert.Equal(1, repo.PropertyCounter);
            mock.VerifyGet(m => m.Products, Times.Once);
        }

        class ModelCompleteFakeRepositoryPricesUnder50 : IRepository
        {
            public IEnumerable<Product> Products { get; } = new Product[] {
                new Product { Name = "Kayak", Price = 5M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };

            public void AddProduct(Product p)
            {
                // do nothing - not required for test
            }
        }

        [Fact]
        public void IndexActionModelIsCompletePricesUnder50()
        {
            // arrange
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepositoryPricesUnder50();

            // act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            // assert
            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }
    }
}
