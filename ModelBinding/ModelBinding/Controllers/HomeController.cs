using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo) {
            repository = repo;
        }

        //public ViewResult Index(int id) => View(repository[id] ?? repository.People.First());

        // FromQuery only accept fragment format ?id=1 (QueryString)
        public IActionResult Index([FromQuery] int? id)
        {
            Person person;
            if (id.HasValue && (person = repository[id.Value]) != null)
            {
                return View(person);
            } else
            {
                return NotFound();
            }
        }

        public ViewResult Create() => View(new Person());

        [HttpPost]
        public ViewResult Create(Person model) => View("Index", model);

        //public ViewResult DisplaySummary([Bind(Prefix = nameof(Person.HomeAddress))]AddressSummary summary) => View(summary);

        // only bind AddressSummary.City
        public ViewResult DisplaySummary([Bind(nameof(AddressSummary.City), Prefix = nameof(Person.HomeAddress))]AddressSummary summary) => View(summary);

        public ViewResult Names(string[] names) => View(names ?? new string[0]);

        public ViewResult NamesCollection(IList<string> names) => View(names ?? new List<string>());

        public ViewResult Address(IList<AddressSummary> addresses) => View(addresses ?? new List<AddressSummary>());

        public string Header([FromHeader]string accept) => $"Header: {accept}";

        public string HeaderName([FromHeader(Name = "Accept-Language")]string accept) => $"Accept-Language: { accept}";

        public ViewResult HeaderModel(HeaderModel model) => View(model);

        public ViewResult Body() => View();

        [HttpPost]
        public Person Body([FromBody]Person model) => model;

    }
}
