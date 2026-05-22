using KentKart.Api.Data;
using KentKart.Api.DTOs.Cards;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.Cards;

public class CardService : ICardService
{
    private readonly KentKartDbContext _context;

    public CardService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<List<CardResponseDto>> GetMyCardsAsync(int userId)
    {
        var cards = await _context.Cards
            .Include(c => c.User)
            .Include(c => c.CardType)
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return cards.Select(MapToDto).ToList();
    }

    public async Task<CardResponseDto> GetMyCardByIdAsync(int userId, int cardId)
    {
        var card = await _context.Cards
            .Include(c => c.User)
            .Include(c => c.CardType)
            .FirstOrDefaultAsync(c => c.CardId == cardId && c.UserId == userId);

        if (card == null)
        {
            throw new Exception("Kart bulunamadı veya bu karta erişim yetkiniz yok.");
        }

        return MapToDto(card);
    }

    public async Task<List<CardResponseDto>> GetAllCardsAsync()
    {
        var cards = await _context.Cards
            .Include(c => c.User)
            .Include(c => c.CardType)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return cards.Select(MapToDto).ToList();
    }

    private static CardResponseDto MapToDto(Card card)
    {
        return new CardResponseDto
        {
            CardId = card.CardId,
            UserId = card.UserId,
            FullName = $"{card.User.FirstName} {card.User.LastName}",
            CardTypeId = card.CardTypeId,
            CardTypeName = card.CardType.Name,
            DiscountRate = card.CardType.DiscountRate,
            CardNumber = card.CardNumber,
            Balance = card.Balance,
            Status = card.Status,
            CreatedAt = card.CreatedAt
        };
    }
}