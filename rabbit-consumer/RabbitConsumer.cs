using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbit_consumer;
public static class RabbitConsumer
{
    public static async Task ConsumeRabbitQueue(string queueName, Action<EventingBasicConsumer, BasicDeliverEventArgs> eventHandler, string exchangeName = "", string routingKey = "")
    {
        if (string.IsNullOrWhiteSpace(exchangeName))
            exchangeName = queueName;

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
                exchange: exchangeName,
                type: ExchangeType.Topic,
                durable:false,
                autoDelete: false,
                arguments: null
            );

            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (consumerObj, ea) =>
            {
                try {
                    var consumer = consumerObj as EventingBasicConsumer;
                    eventHandler(consumer!, ea);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    
                    consumer!.Model.BasicPublish(
                        exchange: "error",
                        routingKey: "",
                        body: ea.Body);
                    }
            };

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            await host.RunAsync();
        }
    }
}
