namespace KentKart.Api.Entities;

public class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;

    public Role Role { get; set; } = null!;

    public ICollection<Card> Cards { get; set; } = new List<Card>();

    public ICollection<CardApplication> CardApplications { get; set; } = new List<CardApplication>();
}