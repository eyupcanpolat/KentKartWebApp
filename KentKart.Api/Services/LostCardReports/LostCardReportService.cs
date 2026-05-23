using KentKart.Api.Data;
using KentKart.Api.DTOs.LostCardReports;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.LostCardReports;

public class LostCardReportService : ILostCardReportService
{
    private readonly KentKartDbContext _context;

    public LostCardReportService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<LostCardReportResponseDto> CreateAsync(int userId, CreateLostCardReportDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Reason))
        {
            throw new Exception("Kayıp bildirim nedeni boş olamaz.");
        }

        var card = await _context.Cards
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.CardId == dto.CardId && c.UserId == userId);

        if (card == null)
        {
            throw new Exception("Kart bulunamadı veya bu karta erişim yetkiniz yok.");
        }

        if (card.Status == "Lost")
        {
            throw new Exception("Bu kart zaten kayıp olarak bildirilmiş.");
        }

        var existingReport = await _context.LostCardReports
            .AnyAsync(lcr => lcr.CardId == dto.CardId && lcr.Status == "Reported");

        if (existingReport)
        {
            throw new Exception("Bu kart için zaten aktif bir kayıp bildirimi var.");
        }

        var report = new LostCardReport
        {
            CardId = dto.CardId,
            UserId = userId,
            Reason = dto.Reason.Trim(),
            Status = "Reported"
        };

        _context.LostCardReports.Add(report);
        await _context.SaveChangesAsync();

        await _context.Entry(card).ReloadAsync();

        var createdReport = await _context.LostCardReports
            .Include(lcr => lcr.Card)
            .Include(lcr => lcr.User)
            .FirstAsync(lcr => lcr.LostCardReportId == report.LostCardReportId);

        return MapToDto(createdReport);
    }

    public async Task<List<LostCardReportResponseDto>> GetMyReportsAsync(int userId)
    {
        var reports = await _context.LostCardReports
            .Include(lcr => lcr.Card)
            .Include(lcr => lcr.User)
            .Where(lcr => lcr.UserId == userId)
            .OrderByDescending(lcr => lcr.ReportDate)
            .ToListAsync();

        return reports.Select(MapToDto).ToList();
    }

    public async Task<List<LostCardReportResponseDto>> GetAllReportsAsync()
    {
        var reports = await _context.LostCardReports
            .Include(lcr => lcr.Card)
            .Include(lcr => lcr.User)
            .OrderByDescending(lcr => lcr.ReportDate)
            .ToListAsync();

        return reports.Select(MapToDto).ToList();
    }

    private static LostCardReportResponseDto MapToDto(LostCardReport report)
    {
        return new LostCardReportResponseDto
        {
            LostCardReportId = report.LostCardReportId,
            CardId = report.CardId,
            CardNumber = report.Card.CardNumber,
            UserId = report.UserId,
            FullName = $"{report.User.FirstName} {report.User.LastName}",
            ReportDate = report.ReportDate,
            Reason = report.Reason,
            Status = report.Status,
            CardStatus = report.Card.Status
        };
    }
}