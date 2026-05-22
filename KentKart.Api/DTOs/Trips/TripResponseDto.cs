namespace KentKart.Api.DTOs.Trips;

public class TripResponseDto
{
    public int TripId { get; set; }

    public int CardId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public string CardTypeName { get; set; } = string.Empty;

    public int BusLineId { get; set; }

    public string LineCode { get; set; } = string.Empty;

    public string LineName { get; set; } = string.Empty;

    public int StationId { get; set; }

    public string StationName { get; set; } = string.Empty;

    public decimal FareAmount { get; set; }

    public decimal CurrentBalance { get; set; }

    public DateTime TripDate { get; set; }

    public string Status { get; set; } = string.Empty;
}
