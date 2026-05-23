using KentKart.Api.Data;
using KentKart.Api.DTOs.Subscriptions;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly KentKartDbContext _context;

    public SubscriptionService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubscriptionPlanResponseDto>> GetPlansAsync()
    {
        var plans = await _context.SubscriptionPlans
            .Include(sp => sp.CardType)
            .Where(sp => sp.IsActive)
            .OrderBy(sp => sp.Price)
            .ToListAsync();

        return plans.Select(sp => new SubscriptionPlanResponseDto
        {
            SubscriptionPlanId = sp.SubscriptionPlanId,
            Name = sp.Name,
            Description = sp.Description,
            CardTypeId = sp.CardTypeId,
            CardTypeName = sp.CardType.Name,
            Price = sp.Price,
            RideCount = sp.RideCount,
            ValidityDays = sp.ValidityDays,
            IsActive = sp.IsActive
        }).ToList();
    }

    public async Task<CardSubscriptionResponseDto> BuySubscriptionAsync(int userId, BuySubscriptionDto dto)
    {
        if (dto.PaymentMethod != "CreditCard" &&
            dto.PaymentMethod != "DebitCard" &&
            dto.PaymentMethod != "BankTransfer")
        {
            throw new Exception("Geçerli ödeme yöntemi seçiniz: CreditCard, DebitCard veya BankTransfer.");
        }

        var card = await _context.Cards
            .Include(c => c.CardType)
            .FirstOrDefaultAsync(c => c.CardId == dto.CardId && c.UserId == userId);

        if (card == null)
        {
            throw new Exception("Kart bulunamadı veya bu karta erişim yetkiniz yok.");
        }

        if (card.Status != "Active")
        {
            throw new Exception("Sadece aktif kartlara abonman alınabilir.");
        }

        var plan = await _context.SubscriptionPlans
            .Include(sp => sp.CardType)
            .FirstOrDefaultAsync(sp => sp.SubscriptionPlanId == dto.SubscriptionPlanId && sp.IsActive);

        if (plan == null)
        {
            throw new Exception("Abonman paketi bulunamadı.");
        }

        if (plan.CardTypeId != card.CardTypeId)
        {
            throw new Exception("Bu abonman paketi kart tipinizle uyumlu değil.");
        }

        var activeSubscriptionExists = await _context.CardSubscriptions
            .AnyAsync(cs =>
                cs.CardId == dto.CardId &&
                cs.Status == "Active" &&
                cs.EndDate >= DateTime.Now &&
                cs.RemainingRideCount > 0);

        if (activeSubscriptionExists)
        {
            throw new Exception("Bu kartta zaten aktif bir abonman var.");
        }

        var subscription = new CardSubscription
        {
            CardId = dto.CardId,
            SubscriptionPlanId = dto.SubscriptionPlanId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(plan.ValidityDays),
            RemainingRideCount = plan.RideCount,
            Status = "Active"
        };

        var payment = new Payment
        {
            CardId = dto.CardId,
            Amount = plan.Price,
            PaymentMethod = dto.PaymentMethod,
            PaymentType = "Subscription",
            Status = "Success",
            Description = $"{plan.Name} abonman satın alma işlemi"
        };

        _context.CardSubscriptions.Add(subscription);
        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        var createdSubscription = await _context.CardSubscriptions
            .Include(cs => cs.Card)
            .Include(cs => cs.SubscriptionPlan)
            .FirstAsync(cs => cs.CardSubscriptionId == subscription.CardSubscriptionId);

        return MapToDto(createdSubscription);
    }

    public async Task<List<CardSubscriptionResponseDto>> GetMySubscriptionsAsync(int userId)
    {
        var subscriptions = await _context.CardSubscriptions
            .Include(cs => cs.Card)
            .Include(cs => cs.SubscriptionPlan)
            .Where(cs => cs.Card.UserId == userId)
            .OrderByDescending(cs => cs.CreatedAt)
            .ToListAsync();

        return subscriptions.Select(MapToDto).ToList();
    }

    private static CardSubscriptionResponseDto MapToDto(CardSubscription subscription)
    {
        return new CardSubscriptionResponseDto
        {
            CardSubscriptionId = subscription.CardSubscriptionId,
            CardId = subscription.CardId,
            CardNumber = subscription.Card.CardNumber,
            SubscriptionPlanId = subscription.SubscriptionPlanId,
            PlanName = subscription.SubscriptionPlan.Name,
            Price = subscription.SubscriptionPlan.Price,
            RemainingRideCount = subscription.RemainingRideCount,
            StartDate = subscription.StartDate,
            EndDate = subscription.EndDate,
            Status = subscription.Status
        };
    }
}