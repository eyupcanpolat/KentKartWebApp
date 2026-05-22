using KentKart.Api.DTOs.Trips;

namespace KentKart.Api.Services.Trips;

public interface ITripService
{
    Task<TripResponseDto> CreateTripAsync(int userId, CreateTripDto dto);

    Task<List<TripResponseDto>> GetMyTripsAsync(int userId);
}