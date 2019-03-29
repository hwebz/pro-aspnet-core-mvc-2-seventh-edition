using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // arrange - create a mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // arrange - create an empty cart
            Cart cart = new Cart();
            // arrange - create the order
            Order order = new Order();
            // arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // act
            ViewResult result = target.Checkout(order) as ViewResult;

            // assert - check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // arrange - create a mock order repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);
            // arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // act - try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // assert - check that the order hasn't been passed stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            // assert - check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // arrange - create a mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // act - try to checkout
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            // assert - check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            // assert - check that the method is redirecting to the Completed action
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
