using KentKart.Api.DTOs.Admin;

namespace KentKart.Api.Services.Admin;

public interface IAdminService
{
    Task<List<BusLineResponseDto>> GetBusLinesAsync();
    Task<BusLineResponseDto> CreateBusLineAsync(CreateBusLineDto dto);
    Task<BusLineResponseDto> UpdateBusLineAsync(int busLineId, UpdateBusLineDto dto);

    Task<List<StationResponseDto>> GetStationsAsync();
    Task<StationResponseDto> CreateStationAsync(CreateStationDto dto);
    Task<StationResponseDto> UpdateStationAsync(int stationId, UpdateStationDto dto);

    Task<List<FareRuleResponseDto>> GetFareRulesAsync();
    Task<FareRuleResponseDto> CreateFareRuleAsync(CreateFareRuleDto dto);
    Task<FareRuleResponseDto> UpdateFareRuleAsync(int fareRuleId, UpdateFareRuleDto dto);
}