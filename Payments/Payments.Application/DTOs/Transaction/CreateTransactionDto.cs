namespace Payments.Application.DTOs.Transaction;

public class CreateTransactionDto
{
    public Guid PaymentId { get; set; }
    
    public string TransactionType { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
    
    public string Status { get; set; } = string.Empty;
    
    public string? GatewayTransactionId { get; set; }
    
    public string? GatewayResponse { get; set; }
    
    public string? Notes { get; set; }
}
