namespace KentKart.Api.Entities;

public class BusLine
{
    public int BusLineId { get; set; }

    public string LineCode { get; set; } = string.Empty;

    public string LineName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<LineStation> LineStations { get; set; } = new List<LineStation>();

    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}