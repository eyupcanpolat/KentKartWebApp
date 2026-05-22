namespace KentKart.Api.Entities;

public class FareRule
{
    public int FareRuleId { get; set; }

    public int CardTypeId { get; set; }

    public decimal Price { get; set; }

    public DateTime ValidFrom { get; set; } = DateTime.Now;

    public DateTime? ValidTo { get; set; }

    public bool IsActive { get; set; } = true;

    public CardType CardType { get; set; } = null!;
}