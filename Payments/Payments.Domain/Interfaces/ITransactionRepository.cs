using Payments.Domain.Entities;

namespace Payments.Domain.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetTransactionsByPaymentIdAsync(Guid paymentId);
}
