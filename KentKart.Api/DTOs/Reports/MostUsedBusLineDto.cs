namespace KentKart.Api.DTOs.Reports;

public class MostUsedBusLineDto
{
    public int BusLineId { get; set; }

    public string LineCode { get; set; } = string.Empty;

    public string LineName { get; set; } = string.Empty;

    public int TripCount { get; set; }

    public decimal TotalFareAmount { get; set; }
}