namespace KentKart.Api.DTOs.Reports;

public class DailyRevenueDto
{
    public DateTime RevenueDate { get; set; }

    public int PaymentCount { get; set; }

    public decimal TotalRevenue { get; set; }
}