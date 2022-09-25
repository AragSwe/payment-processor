await rabbit_consumer.RabbitConsumer.ConsumeRabbitQueue(
    queueName: "internal",
    routingKey: "internal.sek",
    eventHandler: (consumer, ea) =>
{
    var body = ea.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
});