using KentKart.Api.Data;
using KentKart.Api.DTOs.Admin;
using KentKart.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace KentKart.Api.Services.Admin;

public class AdminService : IAdminService
{
    private readonly KentKartDbContext _context;

    public AdminService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<List<BusLineResponseDto>> GetBusLinesAsync()
    {
        var busLines = await _context.BusLines
            .OrderBy(bl => bl.LineCode)
            .ToListAsync();

        return busLines.Select(bl => new BusLineResponseDto
        {
            BusLineId = bl.BusLineId,
            LineCode = bl.LineCode,
            LineName = bl.LineName,
            Description = bl.Description,
            IsActive = bl.IsActive,
            CreatedAt = bl.CreatedAt
        }).ToList();
    }

    public async Task<BusLineResponseDto> CreateBusLineAsync(CreateBusLineDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.LineCode) || string.IsNullOrWhiteSpace(dto.LineName))
        {
            throw new Exception("Hat kodu ve hat adı zorunludur.");
        }

        var exists = await _context.BusLines
            .AnyAsync(bl => bl.LineCode == dto.LineCode.Trim());

        if (exists)
        {
            throw new Exception("Bu hat kodu zaten kullanılıyor.");
        }

        var busLine = new BusLine
        {
            LineCode = dto.LineCode.Trim(),
            LineName = dto.LineName.Trim(),
            Description = dto.Description,
            IsActive = true
        };

        _context.BusLines.Add(busLine);
        await _context.SaveChangesAsync();

        return new BusLineResponseDto
        {
            BusLineId = busLine.BusLineId,
            LineCode = busLine.LineCode,
            LineName = busLine.LineName,
            Description = busLine.Description,
            IsActive = busLine.IsActive,
            CreatedAt = busLine.CreatedAt
        };
    }

    public async Task<BusLineResponseDto> UpdateBusLineAsync(int busLineId, UpdateBusLineDto dto)
    {
        var busLine = await _context.BusLines.FirstOrDefaultAsync(bl => bl.BusLineId == busLineId);

        if (busLine == null)
        {
            throw new Exception("Hat bulunamadı.");
        }

        if (string.IsNullOrWhiteSpace(dto.LineCode) || string.IsNullOrWhiteSpace(dto.LineName))
        {
            throw new Exception("Hat kodu ve hat adı zorunludur.");
        }

        var codeExists = await _context.BusLines
            .AnyAsync(bl => bl.LineCode == dto.LineCode.Trim() && bl.BusLineId != busLineId);

        if (codeExists)
        {
            throw new Exception("Bu hat kodu başka bir hatta kullanılıyor.");
        }

        busLine.LineCode = dto.LineCode.Trim();
        busLine.LineName = dto.LineName.Trim();
        busLine.Description = dto.Description;
        busLine.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return new BusLineResponseDto
        {
            BusLineId = busLine.BusLineId,
            LineCode = busLine.LineCode,
            LineName = busLine.LineName,
            Description = busLine.Description,
            IsActive = busLine.IsActive,
            CreatedAt = busLine.CreatedAt
        };
    }

    public async Task<List<StationResponseDto>> GetStationsAsync()
    {
        var stations = await _context.Stations
            .OrderBy(s => s.StationName)
            .ToListAsync();

        return stations.Select(s => new StationResponseDto
        {
            StationId = s.StationId,
            StationName = s.StationName,
            District = s.District,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt
        }).ToList();
    }

    public async Task<StationResponseDto> CreateStationAsync(CreateStationDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.StationName))
        {
            throw new Exception("Durak adı zorunludur.");
        }

        var station = new Station
        {
            StationName = dto.StationName.Trim(),
            District = dto.District,
            IsActive = true
        };

        _context.Stations.Add(station);
        await _context.SaveChangesAsync();

        return new StationResponseDto
        {
            StationId = station.StationId,
            StationName = station.StationName,
            District = station.District,
            IsActive = station.IsActive,
            CreatedAt = station.CreatedAt
        };
    }

    public async Task<StationResponseDto> UpdateStationAsync(int stationId, UpdateStationDto dto)
    {
        var station = await _context.Stations.FirstOrDefaultAsync(s => s.StationId == stationId);

        if (station == null)
        {
            throw new Exception("Durak bulunamadı.");
        }

        if (string.IsNullOrWhiteSpace(dto.StationName))
        {
            throw new Exception("Durak adı zorunludur.");
        }

        station.StationName = dto.StationName.Trim();
        station.District = dto.District;
        station.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return new StationResponseDto
        {
            StationId = station.StationId,
            StationName = station.StationName,
            District = station.District,
            IsActive = station.IsActive,
            CreatedAt = station.CreatedAt
        };
    }

    public async Task<List<FareRuleResponseDto>> GetFareRulesAsync()
    {
        var fareRules = await _context.FareRules
            .Include(fr => fr.CardType)
            .OrderBy(fr => fr.CardType.Name)
            .ToListAsync();

        return fareRules.Select(fr => new FareRuleResponseDto
        {
            FareRuleId = fr.FareRuleId,
            CardTypeId = fr.CardTypeId,
            CardTypeName = fr.CardType.Name,
            Price = fr.Price,
            ValidFrom = fr.ValidFrom,
            ValidTo = fr.ValidTo,
            IsActive = fr.IsActive
        }).ToList();
    }

    public async Task<FareRuleResponseDto> CreateFareRuleAsync(CreateFareRuleDto dto)
    {
        if (dto.Price <= 0)
        {
            throw new Exception("Ücret 0'dan büyük olmalıdır.");
        }

        var cardType = await _context.CardTypes.FirstOrDefaultAsync(ct => ct.CardTypeId == dto.CardTypeId);

        if (cardType == null)
        {
            throw new Exception("Kart tipi bulunamadı.");
        }

        var fareRule = new FareRule
        {
            CardTypeId = dto.CardTypeId,
            Price = dto.Price,
            ValidFrom = dto.ValidFrom,
            ValidTo = dto.ValidTo,
            IsActive = true
        };

        _context.FareRules.Add(fareRule);
        await _context.SaveChangesAsync();

        return new FareRuleResponseDto
        {
            FareRuleId = fareRule.FareRuleId,
            CardTypeId = fareRule.CardTypeId,
            CardTypeName = cardType.Name,
            Price = fareRule.Price,
            ValidFrom = fareRule.ValidFrom,
            ValidTo = fareRule.ValidTo,
            IsActive = fareRule.IsActive
        };
    }

    public async Task<FareRuleResponseDto> UpdateFareRuleAsync(int fareRuleId, UpdateFareRuleDto dto)
    {
        var fareRule = await _context.FareRules
            .Include(fr => fr.CardType)
            .FirstOrDefaultAsync(fr => fr.FareRuleId == fareRuleId);

        if (fareRule == null)
        {
            throw new Exception("Ücret tarifesi bulunamadı.");
        }

        if (dto.Price <= 0)
        {
            throw new Exception("Ücret 0'dan büyük olmalıdır.");
        }

        fareRule.Price = dto.Price;
        fareRule.ValidFrom = dto.ValidFrom;
        fareRule.ValidTo = dto.ValidTo;
        fareRule.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return new FareRuleResponseDto
        {
            FareRuleId = fareRule.FareRuleId,
            CardTypeId = fareRule.CardTypeId,
            CardTypeName = fareRule.CardType.Name,
            Price = fareRule.Price,
            ValidFrom = fareRule.ValidFrom,
            ValidTo = fareRule.ValidTo,
            IsActive = fareRule.IsActive
        };
    }
}