using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        // dotnet ef migrations add Initial --project SportsStore // Create Initial migration for Products
        public DbSet<Product> Products { get; set; }

        // dotnet ef migrations add Orders --project SportsStore // Add another migration for Orders
        // dotnet ef database drop --force --project SportsStore // drop current database
        // dotnet ef database update --project SportsStore // update database with new one
        public DbSet<Order> Orders { get; set; }
    }
}
