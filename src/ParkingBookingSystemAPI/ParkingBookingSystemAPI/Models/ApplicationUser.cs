using Microsoft.AspNetCore.Identity;

namespace ParkingBookingSystemAPI.Models
{
    public class ApplicationUser:IdentityUser   
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? Birthday { get; set; }

        public ICollection<Car> Cars { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
    