namespace KentKart.Api.DTOs.LostCardReports;

public class LostCardReportResponseDto
{
    public int LostCardReportId { get; set; }

    public int CardId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public DateTime ReportDate { get; set; }

    public string Reason { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string CardStatus { get; set; } = string.Empty;
}