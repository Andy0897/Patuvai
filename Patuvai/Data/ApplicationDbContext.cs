using Microsoft.EntityFrameworkCore;
using Patuvai.Models;

namespace Patuvai.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<UserPolicyAcceptance> UserPolicyAcceptances { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<RequestListing> RequestListings { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Премахваме каскадното изтриване между Review и Driver
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Driver)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DriverId)
                .OnDelete(DeleteBehavior.Restrict); // или DeleteBehavior.NoAction

            // Или премахваме каскадното изтриване между Review и Reviewer (ApplicationUser)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany()
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestListing>()
                .HasOne(r => r.ToCity)
                .WithMany()
                .HasForeignKey(r => r.ToCityId)
                .OnDelete(DeleteBehavior.Restrict); // Or DeleteBehavior.NoAction

            // If you also have FromCity, you can keep cascade on one, or restrict both
            modelBuilder.Entity<RequestListing>()
                .HasOne(r => r.FromCity)
                .WithMany()
                .HasForeignKey(r => r.FromCityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne(r => r.ListingUser)
                .WithMany()
                .HasForeignKey(r => r.ListingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne(r => r.ListingOwner)
                .WithMany()
                .HasForeignKey(r => r.ListingOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
