namespace payment_models;
public record Payment : PaymentOrder
{
    public string Suti { get; set; } = "";
}