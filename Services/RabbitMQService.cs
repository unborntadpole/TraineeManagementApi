using RabbitMQ.Client;
using System.Text.Json;
namespace TraineeManagementApi.Services;

public class RabbitMQProducer
{
    private IConnection _connection;

    public RabbitMQProducer( IConnection connection)
    {
        _connection = connection;   
    }

    public async Task PublishAsync<T>(string exchange, string routingKey, T message) where T : class
    {
        using var channel = await _connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic, durable: true);

        await channel.QueueDeclareAsync(
            queue: "submission-processing", 
            durable: true,
            exclusive: false, 
            autoDelete: false, 
            arguments: null);

        await channel.QueueBindAsync(
            queue: "submission-processing", 
            exchange: exchange, 
            routingKey: routingKey);

        var body = JsonSerializer.SerializeToUtf8Bytes(message);
        var properties = new BasicProperties { DeliveryMode = DeliveryModes.Persistent };

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: true,
            basicProperties: properties,
            body: body);
    }

}