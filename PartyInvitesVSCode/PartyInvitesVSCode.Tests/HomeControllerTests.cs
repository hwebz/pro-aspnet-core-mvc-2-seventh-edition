using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PartyInvitesVSCode.Controllers;
using PartyInvitesVSCode.Models;
using Xunit;
using System.Linq;

namespace PartyInvitesVSCode.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void ListActionFiltersNonAttendees()
        {
            // arrange
            HomeController controller = new HomeController(new FakeRepository());
            // act
            ViewResult result = controller.ListResponses();
            // assert
            Assert.Equal(2, (result.Model as IEnumerable<GuestResponse>).Count());
        }
    }

    class FakeRepository : IRepository
    {
        public IEnumerable<GuestResponse> Responses => new List<GuestResponse> {
            new GuestResponse { Name = "Bob", WillAttend = true },
            new GuestResponse { Name = "Alice", WillAttend = true },
            new GuestResponse { Name = "Joe", WillAttend = false }
        };

        public void AddResponse(GuestResponse response)
        {
            
        }
    }
}
