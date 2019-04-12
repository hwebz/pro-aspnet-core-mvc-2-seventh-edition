using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DependencyInjection.Controllers
{
    public class ActionInjectionController : Controller
    {
        //private IRepository repository;

        //public ActionInjectionController(IRepository repo) => repository = repo;
        public ViewResult Index([FromServices]ProductTotalizer totalizer)
        {
            IRepository repository = (IRepository)HttpContext.RequestServices.GetService(typeof(IRepository));
            ViewBag.ActionInjectionController = repository.ToString();
            ViewBag.Totalizer = totalizer.Repository.ToString();
            return View(repository.Products);
        }
    }
}
