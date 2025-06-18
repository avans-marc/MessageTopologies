using RabbitMQ.Client;
using System.Text;

public class SomeoneSinging : IHostedService
{
    private IConnection _connection;
    private IChannel _channel;
    private ILogger<SomeoneSinging> _logger;

    public SomeoneSinging(ILogger<SomeoneSinging> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" }; // Change as needed
        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(queue: "demo-queue", durable: true, exclusive: false, autoDelete: false);


        while (!cancellationToken.IsCancellationRequested)
        {
            string message = "Hello,";
            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(exchange: "", routingKey: "demo-queue", body);

            _logger.LogInformation($"[Someone] {message}");

            await Task.Delay(100);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel!.CloseAsync();
        await _connection!.CloseAsync();
    }
}