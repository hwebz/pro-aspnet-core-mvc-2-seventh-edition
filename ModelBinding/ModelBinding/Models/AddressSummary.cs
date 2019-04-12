using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models
{
    // Only bind City
    [Bind(nameof(City))]
    public class AddressSummary
    {
        public string City { get; set; }

        // Never bind Country
        [BindNever]
        public string Country { get; set; }
    }
}
