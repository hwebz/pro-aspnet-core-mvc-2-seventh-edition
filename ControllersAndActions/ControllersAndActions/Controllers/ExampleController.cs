using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View(DateTime.Now);
        }

        public ViewResult Result() => View((object)"Hello World");

        public RedirectResult Redirect() => RedirectPermanent("/Example/Index");

        public RedirectToRouteResult RedirectionToRoute() => RedirectToRoute(new { controller = "Example", action = "Index", ID = "MyID" });

        public RedirectToActionResult RedirectionToAction() => RedirectToAction(nameof(Index)); // RedirectToAction(nameof(HomeController), nameof(HomeController.Index));

        [HttpPost]
        public RedirectToActionResult ReceiveForm(string name, string city)
        {
            TempData["name"] = name;
            TempData["city"] = city;
            return RedirectToAction(nameof(Data));
        }

        public ViewResult Data()
        {
            string name = TempData["name"] as string;
            string city = TempData["city"] as string;
            return View("Result", $"{name} lives in {city}");
        }

        public JsonResult JsonResponse() => Json(new[] { "Alice", "Bob", "Joe" });

        public ContentResult ContentResponse() => Content("['Alice', 'Bob', 'Joe']", "application/json");

        public ObjectResult ObjectResponse() => Ok(new string[] { "Alice", "Bob", "Joe" });

        public VirtualFileResult VirtualFileResponse() => File("/lib/bootstrap/dist/css/bootstrap.css", "text/css");

        public StatusCodeResult StatusCodeResponse() => StatusCode(StatusCodes.Status404NotFound);

        public StatusCodeResult NotFoundResponse() => NotFound();
    }
}
