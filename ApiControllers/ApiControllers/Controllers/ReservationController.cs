using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiControllers.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiControllers.Controllers
{
    // Using Package Manager Console to test the API
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;

        public ReservationController(IRepository repo) => repository = repo;

        [HttpGet]
        // Invoke-RestMethod http://localhost:7000/api/reservation -Method GET
        public IEnumerable<Reservation> Get() => repository.Reservations;

        [HttpGet("{id}")]
        // Invoke-RestMethod http://localhost:7000/api/reservation/1 -Method GET
        public Reservation Get(int id) => repository[id];

        [HttpPost]
        // Invoke-RestMethod http://localhost:7000/api/reservation -Method POST -Body (@{clientName="Anne"; location="Meeting Room 4"} | ConvertTo-Json) -ContentType "application/json"
        public Reservation Post([FromBody] Reservation res) => repository.AddReservation(new Reservation
        {
            ClientName = res.ClientName,
            Location = res.Location
        });

        [HttpPut]
        // Invoke-RestMethod http://localhost:7000/api/reservation -Method PUT -Body (@{reservationId="1"; clientName="Bob"; location="Media Room"} | ConvertTo-Json) -ContentType "application/json"
        public Reservation Put([FromBody] Reservation res) => repository.UpdateReservation(res);

        [HttpPatch("{id}")]
        // Invoke-RestMethod http://localhost:7000/api/reservation/2 -Method PATCH -Body (@{ op="replace"; path="clientName"; value="Bob"},@{ op="replace"; path="location"; value="Lecture Hall"} | ConvertTo-Json)  -ContentType "application/json"
        public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        // Invoke-RestMethod http://localhost:7000/api/reservation/2 -Method DELETE
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
