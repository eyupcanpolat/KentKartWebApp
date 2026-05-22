using KentKart.Api.Data;
using KentKart.Api.DTOs.Trips;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.Trips;

public class TripService : ITripService
{
    private readonly KentKartDbContext _context;

    public TripService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<TripResponseDto> CreateTripAsync(int userId, CreateTripDto dto)
    {
        var card = await _context.Cards
            .Include(c => c.CardType)
            .FirstOrDefaultAsync(c => c.CardId == dto.CardId && c.UserId == userId);

        if (card == null)
        {
            throw new Exception("Kart bulunamadı veya bu karta erişim yetkiniz yok.");
        }

        if (card.Status != "Active")
        {
            throw new Exception("Sadece aktif kartlarla yolculuk yapılabilir.");
        }

        var busLine = await _context.BusLines
            .FirstOrDefaultAsync(bl => bl.BusLineId == dto.BusLineId && bl.IsActive);

        if (busLine == null)
        {
            throw new Exception("Geçerli bir hat seçiniz.");
        }

        var station = await _context.Stations
            .FirstOrDefaultAsync(s => s.StationId == dto.StationId && s.IsActive);

        if (station == null)
        {
            throw new Exception("Geçerli bir durak seçiniz.");
        }

        var stationBelongsToLine = await _context.LineStations
            .AnyAsync(ls => ls.BusLineId == dto.BusLineId && ls.StationId == dto.StationId);

        if (!stationBelongsToLine)
        {
            throw new Exception("Seçilen durak bu hatta ait değildir.");
        }

        var fareRule = await _context.FareRules
            .FirstOrDefaultAsync(fr =>
                fr.CardTypeId == card.CardTypeId &&
                fr.IsActive &&
                fr.ValidFrom <= DateTime.Now &&
                (fr.ValidTo == null || fr.ValidTo >= DateTime.Now));

        if (fareRule == null)
        {
            throw new Exception("Bu kart tipi için aktif ücret tarifesi bulunamadı.");
        }

        if (card.Balance < fareRule.Price)
        {
            throw new Exception("Kart bakiyesi yetersiz.");
        }

        var trip = new Trip
        {
            CardId = dto.CardId,
            BusLineId = dto.BusLineId,
            StationId = dto.StationId,
            FareAmount = fareRule.Price,
            Status = "Completed"
        };

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        await _context.Entry(card).ReloadAsync();

        var createdTrip = await _context.Trips
            .Include(t => t.Card)
                .ThenInclude(c => c.CardType)
            .Include(t => t.BusLine)
            .Include(t => t.Station)
            .FirstAsync(t => t.TripId == trip.TripId);

        return MapToDto(createdTrip, card.Balance);
    }

    public async Task<List<TripResponseDto>> GetMyTripsAsync(int userId)
    {
        var trips = await _context.Trips
            .Include(t => t.Card)
                .ThenInclude(c => c.CardType)
            .Include(t => t.BusLine)
            .Include(t => t.Station)
            .Where(t => t.Card.UserId == userId)
            .OrderByDescending(t => t.TripDate)
            .ToListAsync();

        return trips.Select(t => MapToDto(t, t.Card.Balance)).ToList();
    }

    private static TripResponseDto MapToDto(Trip trip, decimal currentBalance)
    {
        return new TripResponseDto
        {
            TripId = trip.TripId,
            CardId = trip.CardId,
            CardNumber = trip.Card.CardNumber,
            CardTypeName = trip.Card.CardType.Name,
            BusLineId = trip.BusLineId,
            LineCode = trip.BusLine.LineCode,
            LineName = trip.BusLine.LineName,
            StationId = trip.StationId,
            StationName = trip.Station.StationName,
            FareAmount = trip.FareAmount,
            CurrentBalance = currentBalance,
            TripDate = trip.TripDate,
            Status = trip.Status
        };
    }
}
