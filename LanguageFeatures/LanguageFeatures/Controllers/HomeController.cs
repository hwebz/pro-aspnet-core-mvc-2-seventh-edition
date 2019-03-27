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

        public ViewResult ShoppingCart()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = Product.GetProducts()
            };
            decimal cartTotal = cart.TotalPrices();
            return View("Index", new string[] { $"Total: {cartTotal:C2}" });
        }

        public ViewResult ShoppingCartIEnumerable()
        {
            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };

            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M}
            };

            decimal cartTotal = cart.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Index", new string[]
            {
                $"Cart Total: {cartTotal:C2}",
                $"Array Total: {arrayTotal:C2}"
            });
        }

        public ViewResult FilterResults()
        {
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            decimal arrayTotal = productArray.FilterByPrice(20).TotalPrices();

            return View("Index", new string[] { $"Array Total: {arrayTotal:C2}" });
        }

        public ViewResult LambdaExpression()
        {
            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            Func<Product, bool> nameFilter = delegate (Product prod)
            {
                return prod?.Name?[0] == 'S';
            };

            //decimal priceFilterTotal = productArray.FilterByPrice(20).TotalPrices();
            //decimal nameFilterTotal = productArray.FilterByName('S').TotalPrices();

            //decimal priceFilterTotal = productArray.Filter(FilterByPrice).TotalPrices();
            //decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();

            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            decimal nameFilterTotal = productArray.Filter(p => p?.Name?[0] == 'S').TotalPrices();

            return View("Index", new string[]
            {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}"
            });
        }

        bool FilterByPrice(Product p)
        {
            return (p?.Price ?? 0) >= 20;
        }

        //public ViewResult Linq()
        //{
        //    return View("Index", Product.GetProducts().Select(p => p?.Name));
        //}
        public ViewResult Linq() => View("Index", Product.GetProducts().Select(p => p?.Name));

        public ViewResult TypeInference() {
            var names = new[] { "Kayak", "Lifejacket", "Soccer ball" };
            return View("Index", names);
        }

        public ViewResult AnonymousType()
        {
            var products = new[]
            {
                new { Name = "Kayak", Price = 275M },
                new { Name = "Lifejacket", Price = 48.95M },
                new { Name = "Soccer ball", Price = 19.50M },
                new { Name = "Corner flag", Price = 34.95M }
            };

            return View("Index", products.Select(p => p.Name));
            //return View("Index", products.Select(p => p.GetType().Name));
        }

        public async Task<ViewResult> AsyncAwait()
        {
            long? length = await MyAsyncMethods.GetPageLength();
            return View("Index", new string[] { $"Length: {length}" });
        }

        public ViewResult HardCoding()
        {
            var products = new[] {
                new { Name = "Kayak", Price = 275M },
                new { Name = "Lifejacket", Price = 48.95M },
                new { Name = "Soccer ball", Price = 19.50M },
                new { Name = "Corner flag", Price = 34.95M }
            };

            return View("Index", products.Select(p => $"{nameof(p.Name)}: {p.Name}, {nameof(p.Price)}: {p.Price}"));
        }
    }
}
