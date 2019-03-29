using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationContext context;

        public EFOrderRepository(ApplicationContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product); // when Order object is read, the collection associated with Lines should loaded along with each Product

        public void SaveOrder(Order order)
        {
            // This ensures that Entity Framework Core won’t try to write the deserialized Product objects that are associated with the Order object.
            context.AttachRange(order.Lines.Select(l => l.Product)); // cause data send by HTTPRequest in object, EF doesn't know it as Product. This will track changes from Product, if any changes, update it, if not, nothing happened in database
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
