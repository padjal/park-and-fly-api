using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingBookingSystemAPI.Models
{
    [Table("Parkings")]
    public class Parking
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Phone { get; set; }
        public int MaxCars { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public double PricePerDay { get; set; }
    }
}
