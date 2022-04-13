using Microsoft.EntityFrameworkCore;

using DAL_Layer.Model;
namespace DAL_Layer
{
    public class EventContext: DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventMember> EventMembers { get; set; }
        public DbSet<EventInterest> EventInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<EventMember>().ToTable("EventMembers");
            modelBuilder.Entity<EventInterest>().ToTable("EventInterests");

            modelBuilder.Entity<Event>()
                .HasMany(x => x.Members)
                .WithOne(x => x.Event);
            modelBuilder.Entity<Event>()
                .HasMany(x => x.Interests)
                .WithOne(x => x.Event);
        }
    }
}
