using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllersAndActions.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        //public ViewResult ReceiveForm()
        //{
        //    var name = Request.Form["name"];
        //    var city = Request.Form["city"];

        //    return View("Result", $"{name} lives in {city}");
        //}

        //public void ReceiveForm(string name, string city)
        //{
        //    //return View("Result", $"{name} lives in {city}");

        //    Response.StatusCode = 200;
        //    Response.ContentType = "text/html";
        //    byte[] content = Encoding.ASCII.GetBytes($"<html><body>{name} lives in {city}</body></html>");
        //    Response.Body.WriteAsync(content, 0, content.Length);
        //}

        //public IActionResult ReceiveForm(string name, string city) => new CustomHtmlResult
        //{
        //    Content = $"{name} lives in {city}"
        //};

        public ViewResult ReceiveForm(string name, string city) => View("Result", $"{name} lives in {city}");
    }
}
