using Payments.Application.DTOs.Payment;

namespace Payments.Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
    
    Task<PaymentDto?> GetPaymentByIdAsync(Guid paymentId);
    
    Task<IEnumerable<PaymentDto>> GetPaymentsByUserIdAsync(Guid userId);
    
    Task<IEnumerable<PaymentDto>> GetPaymentsByCourseIdAsync(Guid courseId);
    
    Task<PaymentDto> ProcessPaymentAsync(ProcessPaymentDto processPaymentDto);
    
    Task<PaymentDto> UpdatePaymentStatusAsync(UpdatePaymentStatusDto updateStatusDto);
    
    Task<PaymentDto> RefundPaymentAsync(RefundPaymentDto refundDto);
    
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
    
    Task<bool> DeletePaymentAsync(Guid paymentId);
}
