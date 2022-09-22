using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using IHost host = Host.CreateDefaultBuilder(args)
    .Build();

Task.Delay(10*1000).Wait();

var factory = new ConnectionFactory { HostName = "payment-consumer-rabbit" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: "incoming",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (_, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" Consumed {message}");
    };

    channel.BasicConsume(
        queue: "incoming",
        autoAck: true,
        consumer: consumer
    );

    await host.RunAsync();
}
