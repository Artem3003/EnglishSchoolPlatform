namespace Payments.Application.DTOs.Payment;

public class ProcessPaymentDto
{
    public Guid PaymentId { get; set; }
    
    public string? PaymentMethodToken { get; set; }
    
    public Dictionary<string, string>? AdditionalData { get; set; }
}
