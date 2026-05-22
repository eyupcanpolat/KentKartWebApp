namespace KentKart.Api.Entities;

public class CardType
{
    public int CardTypeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal DiscountRate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Card> Cards { get; set; } = new List<Card>();

    public ICollection<CardApplication> CardApplications { get; set; } = new List<CardApplication>();

    public ICollection<FareRule> FareRules { get; set; } = new List<FareRule>();
}