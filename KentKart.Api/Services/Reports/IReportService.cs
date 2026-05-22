using KentKart.Api.DTOs.Reports;

namespace KentKart.Api.Services.Reports;

public interface IReportService
{
    Task<List<UserDashboardDto>> GetUserDashboardAsync(int userId);

    Task<List<DailyRevenueDto>> GetDailyRevenueReportAsync();

    Task<List<MostUsedBusLineDto>> GetMostUsedBusLinesReportAsync();
}
