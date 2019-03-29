using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever] // prevent the user from supplying values for this property in an HTTP request
        public int OrderID { get; set; }
        public ICollection<CartLine> Lines { get; set; }

        // dotnet ef migrations add ShippedOrders --project SportsStore
        // dotnet ef database drop --force --project SportsStore
        // dotnet ef database update --project SportsStore
        [BindNever]
        public bool Shipped { get; set; }
        
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
