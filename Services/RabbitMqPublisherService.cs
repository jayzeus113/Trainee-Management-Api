using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using TraineeManagement.DTOs;

namespace TraineeManagement.Services;

public interface IMessagePublisher
{
    Task PublishAsync(SubmissionProcessingRequested message);
}

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMqPublisher> _logger;
    private const string QueueName = "submission-processing";

    public RabbitMqPublisher(IConfiguration configuration, ILogger<RabbitMqPublisher> logger)
    {
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"]!,
            Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
            UserName = configuration["RabbitMQ:UserName"]!,
            Password = configuration["RabbitMQ:Password"]!
        };
    }

    public async Task PublishAsync(SubmissionProcessingRequested message)
    {
        try
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            // Setup durable queue
            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent // Surivives broker crash
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: QueueName,
                mandatory: true,
                basicProperties: properties,
                body: body);

            _logger.LogInformation(
                "Successfully published message to RabbitMQ. MsgID: {MessageId}, CorrID: {CorrelationId}, SubID: {SubmissionId}",
                message.MessageId, message.CorrelationId, message.SubmissionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Failed to publish message to RabbitMQ. MsgID: {MessageId}, CorrID: {CorrelationId}. Broker might be unavailable.",
                message.MessageId, message.CorrelationId);
            
            // Re-throw so controller does not commit a false 202 status
            throw new InvalidOperationException("Messaging subsystem unavailable. Cannot queue work.", ex);
        }
    }
}
