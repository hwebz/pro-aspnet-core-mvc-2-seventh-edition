using ControllersAndActions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace ControllersAndActions.Tests
{
    public class ActionTests
    {
        [Fact]
        public void ViewSelected()
        {
            // arrange
            HomeController controller = new HomeController();

            // act
            ViewResult result = controller.ReceiveForm("Admam", "London") as ViewResult;

            // assert
            Assert.Equal("Result", result.ViewName);
        }

        [Fact]
        public void ModelObjectType()
        {
            // arrange
            ExampleController controller = new ExampleController();

            // act
            ViewResult result = controller.Index();

            // assert
            Assert.IsType<DateTime>(result.ViewData.Model);
        }

        [Fact]
        public void ViewBagValues()
        {
            // arrage
            ExampleController example = new ExampleController();

            // act
            ViewResult result = example.Index();

            // assert
            Assert.IsType<string>(result.ViewData["Message"]);
            Assert.Equal("Hello", result.ViewData["Message"]);
            Assert.IsType<DateTime>(result.ViewData["Date"]);
        }

        [Fact]
        public void Redirection()
        {
            // arrange
            ExampleController controller = new ExampleController();

            // act
            RedirectResult result = controller.Redirect();

            // assert
            Assert.Equal("/Example/Index", result.Url);
            Assert.True(result.Permanent);
        }

        [Fact]
        public void RedirectToRouteTest()
        {
            // arrange
            ExampleController example = new ExampleController();

            // act
            RedirectToRouteResult result = example.RedirectionToRoute();

            // assert
            Assert.False(result.Permanent);
            Assert.Equal("Example", result.RouteValues["controller"]);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("MyID", result.RouteValues["ID"]);
        }

        [Fact]
        public void RedirectToActionTest()
        {
            // arrange
            ExampleController example = new ExampleController();

            // act
            RedirectToActionResult result = example.RedirectionToAction();

            // assert
            Assert.False(result.Permanent);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void JsonActionMethod()
        {
            // arrange
            ExampleController example = new ExampleController();

            // act
            JsonResult result = example.JsonResponse();

            // assert
            Assert.Equal(new[] { "Alice", "Bob", "Joe" }, result.Value);
        }

        [Fact]
        public void NotFoundActionMethod()
        {
            // arrange
            ExampleController example = new ExampleController();

            // act
            StatusCodeResult result = example.NotFoundResponse();

            // assert
            Assert.Equal(404, result.StatusCode);
        }
    }
}
