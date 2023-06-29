using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HF_WEB_API.Data;

namespace HF_WEB_API
{
    public class WebDbContext : IdentityDbContext<Account>
    {
        public WebDbContext(DbContextOptions<WebDbContext> dbContext) : base(dbContext) { }

        // Reflect Table
        public DbSet<News> Newses { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        /* public DbSet<Stock> Stocks { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketOwner> TicketOwners { get; set; } */

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Stock>().HasKey(m => new { m.EventId, m.TicketClassId });
            //builder.Entity<TicketOwner>().HasKey(m => new { m.AccountId, m.TicketId });

            base.OnModelCreating(builder);
        }
    }
}
