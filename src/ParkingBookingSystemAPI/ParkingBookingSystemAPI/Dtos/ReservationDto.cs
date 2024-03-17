namespace ParkingBookingSystemAPI.Dtos
{
    public record ReservationDto(
        int ParkingId,
        string CarId,
        DateTime From,
        DateTime To
        );
}
