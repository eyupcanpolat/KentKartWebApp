namespace KentKart.Api.Entities;

public class LineStation
{
    public int LineStationId { get; set; }

    public int BusLineId { get; set; }

    public int StationId { get; set; }

    public int StationOrder { get; set; }

    public BusLine BusLine { get; set; } = null!;

    public Station Station { get; set; } = null!;
}
