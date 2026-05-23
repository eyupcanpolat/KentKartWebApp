namespace KentKart.Api.Entities;

public class LostCardReport
{
    public int LostCardReportId { get; set; }

    public int CardId { get; set; }

    public int UserId { get; set; }

    public DateTime ReportDate { get; set; } = DateTime.Now;

    public string Reason { get; set; } = string.Empty;

    public string Status { get; set; } = "Reported";

    public Card Card { get; set; } = null!;

    public User User { get; set; } = null!;
}
