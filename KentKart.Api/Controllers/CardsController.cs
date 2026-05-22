using KentKart.Api.Services.Cards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KentKart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CardsController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyCards()
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _cardService.GetMyCardsAsync(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("my/{cardId}")]
    public async Task<IActionResult> GetMyCardById(int cardId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _cardService.GetMyCardByIdAsync(userId, cardId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("admin/all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCards()
    {
        var result = await _cardService.GetAllCardsAsync();

        return Ok(result);
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new Exception("Token içinde kullanıcı bilgisi bulunamadı.");
        }

        return Convert.ToInt32(userIdClaim);
    }
}
