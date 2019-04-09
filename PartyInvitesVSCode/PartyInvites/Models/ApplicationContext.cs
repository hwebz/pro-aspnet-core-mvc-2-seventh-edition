using Microsoft.EntityFrameworkCore;

namespace PartyInvitesVSCode.Models {
    public class ApplicationContext : DbContext {
        public ApplicationContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlite("Filename=./PartyInvites.db");
        }

        public DbSet<GuestResponse> Invites {get;set;}
    }
}