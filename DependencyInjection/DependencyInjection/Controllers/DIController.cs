using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DependencyInjection.Controllers
{
    public class DIController : Controller
    {
        private IRepository repository;
        private ProductTotalizer totalizer;

        public DIController(IRepository repo, ProductTotalizer total)
        {
            repository = repo;
            totalizer = total;
        }

        public ViewResult Index()
        {
            ViewBag.DIController = repository.ToString(); // Guid will change each class implementations cause IRepository service use AddTransient<>() // temporarily
            ViewBag.Totalizer = totalizer.Repository.ToString(); // AddScoped() share the same instance through each HTTP request
            ViewBag.Total = totalizer.Total;
            return View(repository.Products);
        }
    }
}
