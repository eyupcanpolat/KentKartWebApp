namespace KentKart.Api.Entities;

public class Card
{
    public int CardId { get; set; }

    public int UserId { get; set; }

    public int CardTypeId { get; set; }

    public string CardNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; } = 0;

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User User { get; set; } = null!;

    public CardType CardType { get; set; } = null!;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();



}