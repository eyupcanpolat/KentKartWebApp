using KentKart.Api.Data;
using KentKart.Api.DTOs.Payments;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly KentKartDbContext _context;

    public PaymentService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentResponseDto> LoadBalanceAsync(int userId, LoadBalanceDto dto)
    {
        if (dto.Amount <= 0)
        {
            throw new Exception("Yüklenecek tutar 0'dan büyük olmalıdır.");
        }

        if (dto.PaymentMethod != "CreditCard" &&
            dto.PaymentMethod != "DebitCard" &&
            dto.PaymentMethod != "BankTransfer")
        {
            throw new Exception("Geçerli ödeme yöntemi seçiniz: CreditCard, DebitCard veya BankTransfer.");
        }

        var card = await _context.Cards
            .FirstOrDefaultAsync(c => c.CardId == dto.CardId && c.UserId == userId);

        if (card == null)
        {
            throw new Exception("Kart bulunamadı veya bu karta erişim yetkiniz yok.");
        }

        if (card.Status != "Active")
        {
            throw new Exception("Sadece aktif kartlara bakiye yüklenebilir.");
        }

        var payment = new Payment
        {
            CardId = dto.CardId,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            PaymentType = "BalanceLoad",
            Status = "Success",
            Description = $"{dto.Amount} TL bakiye yükleme işlemi"
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        await _context.Entry(card).ReloadAsync();

        var updatedCard = card;

        return new PaymentResponseDto
        {
            PaymentId = payment.PaymentId,
            CardId = payment.CardId,
            CardNumber = updatedCard.CardNumber,
            Amount = payment.Amount,
            CurrentBalance = updatedCard.Balance,
            PaymentMethod = payment.PaymentMethod,
            PaymentType = payment.PaymentType,
            Status = payment.Status,
            PaymentDate = payment.PaymentDate,
            Description = payment.Description
        };
    }

    public async Task<List<PaymentResponseDto>> GetMyPaymentsAsync(int userId)
    {
        var payments = await _context.Payments
            .Include(p => p.Card)
            .Where(p => p.Card.UserId == userId)
            .OrderByDescending(p => p.PaymentDate)
            .ToListAsync();

        return payments.Select(p => new PaymentResponseDto
        {
            PaymentId = p.PaymentId,
            CardId = p.CardId,
            CardNumber = p.Card.CardNumber,
            Amount = p.Amount,
            CurrentBalance = p.Card.Balance,
            PaymentMethod = p.PaymentMethod,
            PaymentType = p.PaymentType,
            Status = p.Status,
            PaymentDate = p.PaymentDate,
            Description = p.Description
        }).ToList();
    }
}