using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbit_consumer;
public static class RabbitConsumer
{
    public static async Task ConsumeRabbitQueue(string queueName, EventHandler<BasicDeliverEventArgs> eventHandler)
    {
        using IHost host = Host.CreateDefaultBuilder()
            .Build();

        Task.Delay(10*1000).Wait();

        var factory = new ConnectionFactory { HostName = "payment-consumer-rabbit" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
            
            channel.ExchangeDeclare(
                exchange: queueName,
                type: ExchangeType.Topic,
                durable:false,
                autoDelete: false,
                arguments: null
            );

            channel.QueueBind(queueName, queueName, "", null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += eventHandler;

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            await host.RunAsync();
        }
    }
}
