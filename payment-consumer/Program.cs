using payment_models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

await rabbit_consumer.RabbitConsumer.ConsumeRabbitQueue("incoming", (consumerObj, ea) =>
{
    var consumer = consumerObj as EventingBasicConsumer;
    var body = ea.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    var messageObj = JsonSerializer.Deserialize<Payment>(ea.Body.ToArray());

    if (messageObj is not null)
    {
        messageObj.Suti = "newsuti";
        var newMessage = JsonSerializer.Serialize(messageObj);
        consumer!.Model.BasicPublish(
            exchange: "internal",
            routingKey: $"internal.{messageObj.Currency.ToLower()}",
            body: System.Text.Encoding.UTF8.GetBytes(newMessage));
    }
    else
    {
        consumer!.Model.BasicPublish(
            exchange: "error",
            routingKey: "serailization",
            body: ea.Body);
    }

    Console.WriteLine($"Consumed {message}");
});