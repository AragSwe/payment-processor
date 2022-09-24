await rabbit_consumer.RabbitConsumer.ConsumeRabbitQueue("internal", (consumerObj, ea) =>
{
    var body = ea.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
});