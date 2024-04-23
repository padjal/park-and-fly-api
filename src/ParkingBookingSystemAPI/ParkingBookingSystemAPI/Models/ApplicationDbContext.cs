using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            base.OnModelCreating(builder);
        }
    }
}
