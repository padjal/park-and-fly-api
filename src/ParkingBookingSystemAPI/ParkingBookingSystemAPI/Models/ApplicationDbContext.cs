using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ParkingBookingSystemAPI.Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        override public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<Parking> Parkings{ get; set; } = null!;
        public DbSet<Car> Cars{ get; set; } = null!;
        public DbSet<Reservation> Reservations{ get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>()
                .HasOne(c => c.Owner)
                .WithMany(o => o.Cars)
                .HasForeignKey(c => c.OwnerId)
                .HasPrincipalKey(c => c.Id);
                

            builder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .HasPrincipalKey(r => r.Id);

            builder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId);

            builder.Entity<Reservation>()
                .HasOne(r => r.Parking)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.ParkingId);

            builder.Entity<Parking>()
                .HasMany(p => p.Reservations)
                .WithOne(r => r.Parking)
                .HasPrincipalKey(p => p.Id);

            base.OnModelCreating(builder);
        }
    }
}
