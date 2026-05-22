namespace KentKart.Api.DTOs.Reports;

public class UserDashboardDto
{
    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int CardId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public string CardTypeName { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string CardStatus { get; set; } = string.Empty;
}

