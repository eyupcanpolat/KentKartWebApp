using KentKart.Api.DTOs.LostCardReports;

namespace KentKart.Api.Services.LostCardReports;

public interface ILostCardReportService
{
    Task<LostCardReportResponseDto> CreateAsync(int userId, CreateLostCardReportDto dto);

    Task<List<LostCardReportResponseDto>> GetMyReportsAsync(int userId);

    Task<List<LostCardReportResponseDto>> GetAllReportsAsync();
}