using ParkingBookingSystemAPI.Dtos;
using ParkingBookingSystemAPI.Dtos.Requests;

namespace ParkingBookingSystemAPI.Models
{
    public static class ModelExtensions
    {
        public static ReservationDto ToDto(this Reservation reservation)
        {
            return new ReservationDto(
                reservation.ParkingId,
                reservation.CarId,
                reservation.From,
                reservation.To
                );
        }

        public static Reservation ToBase(this ReservationDto reservationDto, string userId)
        {
            return new Reservation()
            {
                UserId = userId,
                ParkingId = reservationDto.ParkingId,
                CarId = reservationDto.CarId,
                From = reservationDto.From,
                To = reservationDto.To,
            };
        }

        public static Car ToBase(this AddCarRequest carRequest, string userId)
        {
            return new Car()
            {
                RegistrationNumber = carRequest.RegistrationNumber,
                Color = carRequest.Color,
                Brand = carRequest.Brand,
                Model = carRequest.Model,
                OwnerId = userId,
            };
        }

        public static Parking ToBase(this AddParkingRequest parkingRequest)
        {
            return new Parking()
            {
                Name = parkingRequest.Name,
                Address = parkingRequest.Address,
                City = parkingRequest.City,
                Country = parkingRequest.Country,
                Latitude = parkingRequest.Latitude,
                Longitute = parkingRequest.Longitute,
                Phone = parkingRequest.Phone,
                MaxCars = parkingRequest.MaxCars
            };
        }
    }
}
