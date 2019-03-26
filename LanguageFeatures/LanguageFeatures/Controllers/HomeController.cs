using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageFeatures.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public ViewResult Index()
        {
            //return View(new string[] { "C#", "Language", "Features" });
            List<string> results = new List<string>();

            foreach(Product p in Product.GetProducts())
            {
                string name = p?.Name ?? "<No Name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";
                //results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}", name, price, relatedName));
                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }

            return View(results);
        }

        public ViewResult List()
        {
            List<string> results = new List<string>();

            //Dictionary<string, Product> products = new Dictionary<string, Product>()
            //{
            //    { "Kayak", new Product {Name = "Kayak", Price = 275M} },
            //    { "Lifejacket", new Product {Name = "Lifejacket", Price = 48.95M} }
            //};
            Dictionary<string, Product> products = new Dictionary<string, Product>
            {
                ["Kayak"] = new Product {Name = "Kayak", Price = 275M},
                ["Lifejacket"] = new Product {Name = "Lifejacket", Price = 48.95M}
            };

            foreach ((string key, Product p) in products)
            {
                string name = p?.Name ?? "<No Name>";
                decimal? price = p?.Price ?? 0;
                string relatedName = p?.Related?.Name ?? "<None>";
                //results.Add(string.Format("Name: {0}, Price: {1}, Related: {2}", name, price, relatedName));
                results.Add($"Name: {name}, Price: {price}, Related: {relatedName}");
            }

            return View("Index", results);
        }

        public ViewResult PatternMatching()
        {
            List<string> results = new List<string>();
            object[] data = new object[] { 275M, 29.95M, "apple", "orange", 100, 10 };
            for (int i = 0; i < data.Length; i++)
            {
                //if (data[i] is decimal d)
                //{
                //    results.Add(d.ToString());
                //}

            switch(data[i])
                {
                    case decimal decimalValue:
                        results.Add(string.Format("Decimal: {0}", decimalValue));
                        break;
                    case int intValue when intValue > 50:
                        results.Add(string.Format("Int: {0}", intValue));
                        break;
                }
            }

            return View("Index", results);
        }
    }
}
