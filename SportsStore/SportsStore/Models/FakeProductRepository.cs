using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product { Name = "Football", Price = 25 },
            new Product { Name = "Surf board", Price = 179 },
            new Product { Name = "Running shoes", Price = 95 }
        }.AsQueryable<Product>();

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                Products.Concat<Product>(new[] { product });
            } else
            {
                Product entry = Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (entry != null)
                {
                    entry.Name = product.Name;
                    entry.Description = product.Description;
                    entry.Price = product.Price;
                    entry.Category = product.Category;
                }
            }
        }
    }
}
