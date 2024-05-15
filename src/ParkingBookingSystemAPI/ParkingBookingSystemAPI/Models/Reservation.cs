using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingBookingSystemAPI.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int ParkingId { get; set; }
        public string CarId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsPaid { get; set; }
        public double Price { get; set; }
    }
}
