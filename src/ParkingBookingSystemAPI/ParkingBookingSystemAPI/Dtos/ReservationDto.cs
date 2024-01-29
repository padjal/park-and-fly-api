namespace ParkingBookingSystemAPI.Dtos
{
    public record ReservationDto(
        string UserId,
        int ParkingId,
        string CarId,
        DateTime From,
        DateTime To
        );
}
