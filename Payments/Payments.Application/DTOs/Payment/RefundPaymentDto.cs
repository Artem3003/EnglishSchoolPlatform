namespace Payments.Application.DTOs.Payment;

public class RefundPaymentDto
{
    public Guid PaymentId { get; set; }
    
    public decimal? RefundAmount { get; set; }
    
    public string? Reason { get; set; }
}
