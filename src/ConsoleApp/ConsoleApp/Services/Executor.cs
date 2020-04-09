using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Services
{
    public class Executor
    {
        private readonly IServiceA _serviceA;
        private readonly ILogger _logger;

        public Executor(IServiceA serviceA,
            ILogger<Executor> logger)
        {
            _serviceA = serviceA;
            _logger = logger;
        }

        public Task Do()
        {
            _logger.LogInformation("got {number}", _serviceA.Get());
            return Task.CompletedTask;
        }
    }
}
