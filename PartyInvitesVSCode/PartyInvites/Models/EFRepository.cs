using System.Collections.Generic;

namespace PartyInvitesVSCode.Models {
    public class EFRespository : IRepository
    {
        private ApplicationContext context = new ApplicationContext();
        public IEnumerable<GuestResponse> Responses => context.Invites;

        public void AddResponse(GuestResponse response)
        {
            context.Invites.Add(response);
            context.SaveChanges();
        }
    }
}