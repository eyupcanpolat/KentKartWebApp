namespace KentKart.Api.DTOs.Trips;

public class CreateTripDto
{
    public int CardId { get; set; }

    public int BusLineId { get; set; }

    public int StationId { get; set; }
}