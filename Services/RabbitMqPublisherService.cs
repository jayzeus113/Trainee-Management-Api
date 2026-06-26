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
    private readonly IConnection _connection;
    private readonly ILogger<RabbitMqPublisher> _logger;
    private const string QueueName = "submission-processing";
    private const string DlxName = "submission-dlx";
    private const string DlqRoutingKey = "submission-failed";

    public RabbitMqPublisher(IConnection connection, ILogger<RabbitMqPublisher> logger)
    {
        _logger = logger;
        _connection = connection;
    }

    public async Task PublishAsync(SubmissionProcessingRequested message)
    {
        _logger.LogDebug("Preparing payload tracking context. MsgID: {MessageId}. Payload: {PayloadJson}", message.MessageId, JsonSerializer.Serialize(message));

        try
        {
            await using var channel = await _connection.CreateChannelAsync();

            var queueArguments = new Dictionary<string, object?>
            {
                { "x-dead-letter-exchange", DlxName },
                { "x-dead-letter-routing-key", DlqRoutingKey }
            };


            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: queueArguments);


            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            var properties = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
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
                
            throw new InvalidOperationException("Messaging subsystem unavailable. Cannot queue work.", ex);
        }
    }
}
