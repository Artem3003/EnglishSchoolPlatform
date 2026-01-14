namespace Payments.Domain.Entities.Enums;

public enum PaymentMethod
{
    CreditCard = 0,
    DebitCard = 1,
    PayPal = 2,
    BankTransfer = 3,
    Stripe = 4,
    Cash = 5
}
