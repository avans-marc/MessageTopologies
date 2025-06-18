using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FanOut
{
    public class Adele : IConsumer<Lyric>
    {
        private readonly ILogger<Adele> _logger;

        public Adele(ILogger<Adele> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Lyric> context)
        {
            _logger.LogInformation($"[{this.GetType().Name}] It is me...");

            return Task.CompletedTask;
        }
    }
}