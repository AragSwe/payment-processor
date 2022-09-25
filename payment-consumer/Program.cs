using payment_models;
using RabbitMQ.Client;
using System.Text.Json;

await rabbit_consumer.RabbitConsumer.ConsumeRabbitQueue("incoming", (consumer, ea) =>
{
    var body = ea.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    var options = new JsonSerializerOptions {
        PropertyNameCaseInsensitive = true,
    };
    var messageObj = JsonSerializer.Deserialize<Payment>(ea.Body.ToArray(), options);

    if (messageObj is null)
        throw new Exception("MessageBody is not a valid payment.");
    
    messageObj.Suti = "newsuti";
    var newMessage = JsonSerializer.Serialize(messageObj);
    consumer!.Model.BasicPublish(
        exchange: "internal",
        routingKey: $"internal.{messageObj.Currency.ToLower()}",
        body: System.Text.Encoding.UTF8.GetBytes(newMessage));

    Console.WriteLine($"Consumed {message}");
});