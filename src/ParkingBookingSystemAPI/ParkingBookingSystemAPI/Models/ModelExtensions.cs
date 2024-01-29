using ParkingBookingSystemAPI.Dtos;

namespace ParkingBookingSystemAPI.Models
{
    public static class ModelExtensions
    {
        public static ReservationDto ToDto(this Reservation reservation)
        {
            return new ReservationDto(
                reservation.UserId,
                reservation.ParkingId,
                reservation.CarId,
                reservation.From,
                reservation.To
                );
        }

        public static Reservation ToBase(this ReservationDto reservationDto)
        {
            return new Reservation()
            {
                UserId = reservationDto.UserId,
                ParkingId = reservationDto.ParkingId,
                CarId = reservationDto.CarId,
                From = reservationDto.From,
                To = reservationDto.To,
            };
        }
    }
}
