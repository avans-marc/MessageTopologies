using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FanOut
{
    public class SomeoneSinging : IHostedService
    {
        private readonly IBus _bus;
        private readonly ILogger<SomeoneSinging> _logger;

        public SomeoneSinging(IBus bus, ILogger<SomeoneSinging> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var lyric = new Lyric { Text = $"Hello," };
                await _bus.Publish(lyric, cancellationToken);

                _logger.LogInformation($"[Someone] {lyric.Text}");

                await Task.Delay(100, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }


}