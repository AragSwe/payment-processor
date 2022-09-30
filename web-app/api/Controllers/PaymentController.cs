using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using payment_models;
using RabbitMQ.Client;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        this.logger = logger;
    }

    [HttpPost]
    public bool Post([FromBody] PaymentOrder paymentOrder)
    {
        var factory = new ConnectionFactory { HostName = "payment-consumer-rabbit" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var body = JsonSerializer.Serialize(paymentOrder);
            channel.BasicPublish(
                exchange: "incoming",
                routingKey: "",
                mandatory: false,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(body));
            return true;
        }
    }
}
