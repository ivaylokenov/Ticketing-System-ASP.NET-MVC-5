namespace TicketingSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using TicketingSystem.Models;

    public class TicketSystemDbContext : IdentityDbContext<User>
    {
        public TicketSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Ticket> Tickets { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Image> Images { get; set; }
             
        public static TicketSystemDbContext Create()
        {
            return new TicketSystemDbContext();
        }
    }
}
