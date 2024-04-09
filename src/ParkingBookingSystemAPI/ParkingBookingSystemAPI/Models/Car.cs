using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingBookingSystemAPI.Models
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public string RegistrationNumber { get; set; }

        public string? Color { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
    