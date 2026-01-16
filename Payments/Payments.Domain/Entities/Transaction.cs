using Payments.Domain.Entities.Common;

namespace Payments.Domain.Entities;

public class Transaction : BaseEntity<Guid>
{
    public Guid PaymentId { get; set; }
    
    public Payment Payment { get; set; } = null!;
    
    public string TransactionType { get; set; } = string.Empty; // Charge, Refund, Authorization
    
    public decimal Amount { get; set; }
    
    public string Status { get; set; } = string.Empty;
    
    public string? GatewayTransactionId { get; set; }
    
    public string? GatewayResponse { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string? Notes { get; set; }
}
