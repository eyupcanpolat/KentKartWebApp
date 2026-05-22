namespace KentKart.Api.Entities;

public class Trip
{
    public int TripId { get; set; }

    public int CardId { get; set; }

    public int BusLineId { get; set; }

    public int StationId { get; set; }

    public decimal FareAmount { get; set; }

    public DateTime TripDate { get; set; } = DateTime.Now;

    public string Status { get; set; } = "Completed";

    public Card Card { get; set; } = null!;

    public BusLine BusLine { get; set; } = null!;

    public Station Station { get; set; } = null!;
}