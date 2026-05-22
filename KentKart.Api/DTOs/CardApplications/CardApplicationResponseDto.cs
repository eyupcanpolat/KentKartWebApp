namespace KentKart.Api.DTOs.CardApplications;

public class CardApplicationResponseDto
{
    public int CardApplicationId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int CardTypeId { get; set; }

    public string CardTypeName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string? AdminNote { get; set; }

    public DateTime ApplicationDate { get; set; }

    public DateTime? ApprovedAt { get; set; }
}