namespace Payments.Application.DTOs.Transaction;

public class TransactionDto
{
    public Guid Id { get; set; }
    
    public Guid PaymentId { get; set; }
    
    public string TransactionType { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
    
    public string Status { get; set; } = string.Empty;
    
    public string? GatewayTransactionId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string? Notes { get; set; }
}
