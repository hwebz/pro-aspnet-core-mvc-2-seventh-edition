using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiControllers.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    // Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
    // Browser perfers to receive HTML or XHTML data or WEBP images
    // if no formats available, then prefer XML
    // if none, then accept any format
    public class ContentController : Controller
    {
        [HttpGet("string")]
        // api/content/string => text/plain
        // Invoke-WebRequest http://localhost:7000/api/content/string | select @{n='Content-Type';e={ $_.Headers."Content-Type" }}, Content
        public string GetString() => "This is a string response";

        [HttpGet("object")]
        [Produces("application/xml")]
        // api/content/object => application/json
        // Invoke-WebRequest http://localhost:7000/api/content/object | select @{n='Content-Type';e={ $_.Headers."Content-Type" }}, Content
        // Invoke-WebRequest http://localhost:7000/api/content/object -Headers @{Accept="application/xml"} | select @{n='Content-Type';e={ $_.Headers."Content-Type" }}, Content
        // (Invoke-WebRequest http://localhost:7000/api/content/object -Headers @{Accept="application/xml"}).Headers."Content-Type"
        public Reservation GetObject() => new Reservation
        {
            ReservationId = 100,
            ClientName = "Joe",
            Location = "Board Room"
        };

        [HttpGet("object/{format?}")]
        [FormatFilter]
        //[Produces("application/json", "application/xml")]
        // using api/content/object/json or api/content/object/xml
        // (Invoke-WebRequest http://localhost:7000/api/content/object/xml).Headers."Content-Type"
        // (Invoke-WebRequest http://localhost:7000/api/content/object/json).Headers."Content-Type"
        // full content negotiation
        // Invoke-WebRequest http://localhost:7000/api/content/object -Headers @{Accept="application/custom"} => 406 - Not Acceptable
        public Reservation GetObjectFormat() => new Reservation
        {
            ReservationId = 100,
            ClientName = "Joe",
            Location = "Board Room"
        };

        [HttpPost]
        [Consumes("application/json")]
        public Reservation ReceiveJson([FromBody] Reservation reservation)
        {
            reservation.ClientName = "Json";
            return reservation;
        }

        [HttpPost]
        [Consumes("application/xml")]
        public Reservation ReceiveXml([FromBody] Reservation reservation)
        {
            reservation.ClientName = "Xml";
            return reservation;
        }
    }
}
