using Payments.Domain.Entities.Enums;

namespace Payments.Application.DTOs.Payment;

public class PaymentDto
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid? CourseId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; } = string.Empty;
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public PaymentStatus Status { get; set; }
    
    public string? TransactionId { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public string? FailureReason { get; set; }
}
