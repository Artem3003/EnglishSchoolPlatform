using Payments.Domain.Entities.Common;
using Payments.Domain.Entities.Enums;

namespace Payments.Domain.Entities;

public class Payment : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    
    public Guid? CourseId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; } = "USD";
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    
    public string? TransactionId { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public string? FailureReason { get; set; }
    
    public string? PaymentGatewayResponse { get; set; }

    public List<Transaction> Transactions { get; set; } = [];
}
