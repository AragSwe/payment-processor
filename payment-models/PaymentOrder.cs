namespace payment_models;
public record PaymentOrder
{
    public string FromAccount { get; set; } = "";
    public string ToAccount { get; set; } = "";
    public string Currency { get; set; } = "";
    public decimal Amount { get; set; }
}
