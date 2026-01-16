using Microsoft.EntityFrameworkCore;
using Payments.Domain.Data;
using Payments.Domain.Entities;
using Payments.Domain.Interfaces;

namespace Payments.Domain.Repositories;

public class PaymentRepository(PaymentsDbContext context) : AbstractRepository<Payment>(context), IPaymentRepository
{
    private readonly PaymentsDbContext _context = context;

    public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId)
    {
        return await _context.Payments
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByCourseIdAsync(Guid courseId)
    {
        return await _context.Payments
            .Where(p => p.CourseId == courseId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Payment?> GetPaymentWithTransactionsAsync(Guid paymentId)
    {
        return await _context.Payments
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(p => p.Id == paymentId);
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(string status)
    {
        return await _context.Payments
            .Where(p => p.Status.ToString() == status)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}
