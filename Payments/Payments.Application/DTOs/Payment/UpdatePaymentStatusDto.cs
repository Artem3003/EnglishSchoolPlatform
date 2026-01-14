using Payments.Domain.Entities.Enums;

namespace Payments.Application.DTOs.Payment;

public class UpdatePaymentStatusDto
{
    public Guid PaymentId { get; set; }
    
    public PaymentStatus Status { get; set; }
    
    public string? TransactionId { get; set; }
    
    public string? FailureReason { get; set; }
    
    public string? PaymentGatewayResponse { get; set; }
}
