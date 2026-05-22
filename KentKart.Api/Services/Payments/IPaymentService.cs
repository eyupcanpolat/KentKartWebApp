using KentKart.Api.DTOs.Payments;

namespace KentKart.Api.Services.Payments;

public interface IPaymentService
{
    Task<PaymentResponseDto> LoadBalanceAsync(int userId, LoadBalanceDto dto);

    Task<List<PaymentResponseDto>> GetMyPaymentsAsync(int userId);
}