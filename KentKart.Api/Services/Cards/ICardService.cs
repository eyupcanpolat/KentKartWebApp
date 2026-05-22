using KentKart.Api.DTOs.Cards;

namespace KentKart.Api.Services.Cards;

public interface ICardService
{
    Task<List<CardResponseDto>> GetMyCardsAsync(int userId);

    Task<CardResponseDto> GetMyCardByIdAsync(int userId, int cardId);

    Task<List<CardResponseDto>> GetAllCardsAsync();
}