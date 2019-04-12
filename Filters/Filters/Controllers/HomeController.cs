using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filters.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Filters.Controllers
{
    //[RequireHttps]
    //[HttpsOnly]
    //[Profile]
    //[ViewResultDetails]
    //[RangeException]
    //[TypeFilter(typeof(DiagnosticsFilter))]
    //[TypeFilter(typeof(TimerFilter))]
    //[ServiceFilter(typeof(TimerFilter))]
    [Message("This is the Controller-Scoped Filter", Order = 10)]
    public class HomeController : Controller
    {
        //[RequireHttps]
        [Message("This is the First Action-Scoped Filter", Order = 1)]
        [Message("This is the Second Action-Scoped Filter", Order = -1)]
        public ViewResult Index()
        {
            //if (!Request.IsHttps)
            //{
            //    return new StatusCodeResult(StatusCodes.Status403Forbidden);
            //} else
            //{
                return View("Message", "This is the Index action on the Home controller");
            //}
        }

        public ViewResult SecondAction() => View("Message", "This is the SecondAction action on the Home controller");

        public ViewResult GenerateException(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            } else if (id > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            } else
            {
                return View("Message", $"The value is {id}");
            }
        }
    }
}
