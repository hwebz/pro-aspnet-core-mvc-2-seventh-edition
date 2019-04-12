using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsingViewComponents.Models;

namespace UsingViewComponents.Components
{
    public class CitySummary : ViewComponent
    {
        private ICityRepository repository;

        public CitySummary(ICityRepository repo) => repository = repo;

        //public IViewComponentResult Invoke()
        //{
        //    // Home/Index/USA
        //    string target = RouteData.Values["id"] as string;
        //    var cities = repository.Cities.Where(city => target == null || string.Compare(city.Country, target, true) == 0);

        //    //return $"{repository.Cities.Count()} cities, {repository.Cities.Sum(c => c.Population)} people";
        //    // return Content("This is a <h3><i>string</i></h3>"); // Content method encoded to make it safe to include HTML
        //    //return new HtmlContentViewComponentResult(new HtmlString("This is a <h3><i>string</i></h3>"));
        //    return View(new CityViewModel
        //    {
        //        //Cities = repository.Cities.Count(),
        //        //Population = repository.Cities.Sum(c => c.Population)
        //        Cities = cities.Count(),
        //        Population = cities.Sum(c => c.Population)
        //    });
        //}

        public IViewComponentResult Invoke(bool showList)
        {
            if (showList)
            {
                return View("CityList", repository.Cities);
            } else
            {
                return View(new CityViewModel
                {
                    Cities = repository.Cities.Count(),
                    Population = repository.Cities.Sum(c => c.Population)
                });
            }
        }
    }
}
