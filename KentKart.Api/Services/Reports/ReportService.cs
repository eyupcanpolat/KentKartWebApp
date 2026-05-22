using KentKart.Api.Data;
using KentKart.Api.DTOs.Reports;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KentKart.Api.Services.Reports;

public class ReportService : IReportService
{
    private readonly KentKartDbContext _context;

    public ReportService(KentKartDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDashboardDto>> GetUserDashboardAsync(int userId)
    {
        var result = new List<UserDashboardDto>();

        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await using var command = connection.CreateCommand();
        command.CommandText = "dbo.sp_GetUserDashboard";
        command.CommandType = CommandType.StoredProcedure;

        var userIdParameter = command.CreateParameter();
        userIdParameter.ParameterName = "@UserId";
        userIdParameter.Value = userId;
        command.Parameters.Add(userIdParameter);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new UserDashboardDto
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                FullName = reader["FullName"].ToString() ?? string.Empty,
                Email = reader["Email"].ToString() ?? string.Empty,
                CardId = Convert.ToInt32(reader["CardId"]),
                CardNumber = reader["CardNumber"].ToString() ?? string.Empty,
                CardTypeName = reader["CardTypeName"].ToString() ?? string.Empty,
                Balance = Convert.ToDecimal(reader["Balance"]),
                CardStatus = reader["CardStatus"].ToString() ?? string.Empty
            });
        }

        return result;
    }

    public async Task<List<DailyRevenueDto>> GetDailyRevenueReportAsync()
    {
        var result = new List<DailyRevenueDto>();

        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await using var command = connection.CreateCommand();
        command.CommandText = "dbo.sp_GetDailyRevenueReport";
        command.CommandType = CommandType.StoredProcedure;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new DailyRevenueDto
            {
                RevenueDate = Convert.ToDateTime(reader["RevenueDate"]),
                PaymentCount = Convert.ToInt32(reader["PaymentCount"]),
                TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
            });
        }

        return result;
    }

    public async Task<List<MostUsedBusLineDto>> GetMostUsedBusLinesReportAsync()
    {
        var result = new List<MostUsedBusLineDto>();

        var connection = _context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await using var command = connection.CreateCommand();
        command.CommandText = "dbo.sp_GetMostUsedBusLinesReport";
        command.CommandType = CommandType.StoredProcedure;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new MostUsedBusLineDto
            {
                BusLineId = Convert.ToInt32(reader["BusLineId"]),
                LineCode = reader["LineCode"].ToString() ?? string.Empty,
                LineName = reader["LineName"].ToString() ?? string.Empty,
                TripCount = Convert.ToInt32(reader["TripCount"]),
                TotalFareAmount = reader["TotalFareAmount"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(reader["TotalFareAmount"])
            });
        }

        return result;
    }
}