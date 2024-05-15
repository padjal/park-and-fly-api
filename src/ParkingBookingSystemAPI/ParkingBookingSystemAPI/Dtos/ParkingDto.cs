namespace ParkingBookingSystemAPI.Dtos
{
    public record ParkingDto(
        string Name,
        string Address,
        string City,
        string Country,
        string Phone,
        int MaxCars,
        double PricePerDay
        );
}
