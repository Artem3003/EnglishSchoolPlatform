using Payments.Domain.Entities;

namespace Payments.Domain.Interfaces;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId);
    
    Task<IEnumerable<Payment>> GetPaymentsByCourseIdAsync(Guid courseId);
    
    Task<Payment?> GetPaymentWithTransactionsAsync(Guid paymentId);
    
    Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(string status);
}
