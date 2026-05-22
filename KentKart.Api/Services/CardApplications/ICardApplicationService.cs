using KentKart.Api.DTOs.CardApplications;

namespace KentKart.Api.Services.CardApplications;

public interface ICardApplicationService
{
    Task<CardApplicationResponseDto> CreateAsync(int userId, CreateCardApplicationDto dto);

    Task<List<CardApplicationResponseDto>> GetMyApplicationsAsync(int userId);

    Task<List<CardApplicationResponseDto>> GetPendingApplicationsAsync();

    Task<CardApplicationResponseDto> ReviewAsync(int applicationId, ReviewCardApplicationDto dto);
}