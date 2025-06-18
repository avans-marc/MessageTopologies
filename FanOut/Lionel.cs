using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FanOut
{
    public class Lionel : IConsumer<Lyric>
    {
        private readonly ILogger<Lionel> _logger;

        public Lionel(ILogger<Lionel> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Lyric> context)
        {
            _logger.LogInformation($"[{this.GetType().Name}] is it me you're looking for");

            return Task.CompletedTask;
        }
    }
}