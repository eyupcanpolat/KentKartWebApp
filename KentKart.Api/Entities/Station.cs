namespace KentKart.Api.Entities;

public class Station
{
    public int StationId { get; set; }

    public string StationName { get; set; } = string.Empty;

    public string? District { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<LineStation> LineStations { get; set; } = new List<LineStation>();

    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}