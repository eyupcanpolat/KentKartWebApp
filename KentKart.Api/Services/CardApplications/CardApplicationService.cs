using KentKart.Api.Data;
using KentKart.Api.DTOs.CardApplications;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.CardApplications;

public class CardApplicationService : ICardApplicationService
{
    private readonly KentKartDbContext _context;

    public CardApplicationService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<CardApplicationResponseDto> CreateAsync(int userId, CreateCardApplicationDto dto)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.UserId == userId && u.IsActive);

        if (!userExists)
        {
            throw new Exception("Kullanıcı bulunamadı veya aktif değil.");
        }

        var cardType = await _context.CardTypes
            .FirstOrDefaultAsync(ct => ct.CardTypeId == dto.CardTypeId && ct.IsActive);

        if (cardType == null)
        {
            throw new Exception("Geçerli bir kart tipi seçiniz.");
        }

        var pendingApplicationExists = await _context.CardApplications
            .AnyAsync(ca =>
                ca.UserId == userId &&
                ca.CardTypeId == dto.CardTypeId &&
                ca.Status == "Pending");

        if (pendingApplicationExists)
        {
            throw new Exception("Bu kart tipi için bekleyen bir başvurunuz zaten var.");
        }

        var application = new CardApplication
        {
            UserId = userId,
            CardTypeId = dto.CardTypeId,
            Status = "Pending"
        };

        _context.CardApplications.Add(application);
        await _context.SaveChangesAsync();

        var createdApplication = await _context.CardApplications
            .Include(ca => ca.User)
            .Include(ca => ca.CardType)
            .FirstAsync(ca => ca.CardApplicationId == application.CardApplicationId);

        return MapToDto(createdApplication);
    }

    public async Task<List<CardApplicationResponseDto>> GetMyApplicationsAsync(int userId)
    {
        var applications = await _context.CardApplications
            .Include(ca => ca.User)
            .Include(ca => ca.CardType)
            .Where(ca => ca.UserId == userId)
            .OrderByDescending(ca => ca.ApplicationDate)
            .ToListAsync();

        return applications.Select(MapToDto).ToList();
    }

    public async Task<List<CardApplicationResponseDto>> GetPendingApplicationsAsync()
    {
        var applications = await _context.CardApplications
            .Include(ca => ca.User)
            .Include(ca => ca.CardType)
            .Where(ca => ca.Status == "Pending")
            .OrderByDescending(ca => ca.ApplicationDate)
            .ToListAsync();

        return applications.Select(MapToDto).ToList();
    }

    public async Task<CardApplicationResponseDto> ReviewAsync(int applicationId, ReviewCardApplicationDto dto)
    {
        if (dto.Status != "Approved" && dto.Status != "Rejected")
        {
            throw new Exception("Status sadece Approved veya Rejected olabilir.");
        }

        var application = await _context.CardApplications
            .Include(ca => ca.User)
            .Include(ca => ca.CardType)
            .FirstOrDefaultAsync(ca => ca.CardApplicationId == applicationId);

        if (application == null)
        {
            throw new Exception("Başvuru bulunamadı.");
        }

        if (application.Status != "Pending")
        {
            throw new Exception("Sadece bekleyen başvurular değerlendirilebilir.");
        }

        application.Status = dto.Status;
        application.AdminNote = dto.AdminNote;

        if (dto.Status == "Approved")
        {
            application.ApprovedAt = DateTime.Now;

            var card = new Card
            {
                UserId = application.UserId,
                CardTypeId = application.CardTypeId,
                CardNumber = await GenerateUniqueCardNumberAsync(),
                Balance = 0,
                Status = "Active"
            };

            _context.Cards.Add(card);
        }

        await _context.SaveChangesAsync();

        return MapToDto(application);
    }

    private async Task<string> GenerateUniqueCardNumberAsync()
    {
        var random = new Random();
        string cardNumber;

        do
        {
            cardNumber = "41" + random.Next(100000000, 999999999).ToString();
        }
        while (await _context.Cards.AnyAsync(c => c.CardNumber == cardNumber));

        return cardNumber;
    }

    private static CardApplicationResponseDto MapToDto(CardApplication application)
    {
        return new CardApplicationResponseDto
        {
            CardApplicationId = application.CardApplicationId,
            UserId = application.UserId,
            FullName = $"{application.User.FirstName} {application.User.LastName}",
            CardTypeId = application.CardTypeId,
            CardTypeName = application.CardType.Name,
            Status = application.Status,
            AdminNote = application.AdminNote,
            ApplicationDate = application.ApplicationDate,
            ApprovedAt = application.ApprovedAt
        };
    }
}