using Payments.Application.DTOs.Transaction;

namespace Payments.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto createTransactionDto);
    
    Task<TransactionDto?> GetTransactionByIdAsync(Guid transactionId);
    
    Task<IEnumerable<TransactionDto>> GetTransactionsByPaymentIdAsync(Guid paymentId);
    
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
}
