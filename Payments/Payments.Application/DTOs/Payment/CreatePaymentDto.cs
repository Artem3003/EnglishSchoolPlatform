using Payments.Domain.Entities.Enums;

namespace Payments.Application.DTOs.Payment;

public class CreatePaymentDto
{
    public Guid UserId { get; set; }
    
    public Guid? CourseId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; } = "USD";
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public string? Description { get; set; }
}
