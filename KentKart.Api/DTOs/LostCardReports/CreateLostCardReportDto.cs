namespace KentKart.Api.DTOs.LostCardReports;

public class CreateLostCardReportDto
{
    public int CardId { get; set; }

    public string Reason { get; set; } = string.Empty;
}