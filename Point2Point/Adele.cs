using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Adele : IHostedService
{
    private IConnection _connection;
    private IChannel _channel;

    private ILogger<Adele> _logger;

    public Adele(ILogger<Adele> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" }; // Change as needed
        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync();


        await _channel.QueueDeclareAsync(queue: "demo-queue", durable: true, exclusive: false, autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async(sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"[{this.GetType().Name}]: It is me...");
        };

        await _channel.BasicConsumeAsync(queue: "demo-queue", autoAck: true, consumer: consumer);

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel!.CloseAsync();
        await _connection!.CloseAsync();
    }
}
