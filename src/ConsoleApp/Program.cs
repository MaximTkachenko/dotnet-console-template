using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{(DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds}.log"))
                .CreateLogger();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IServiceA, ServiceA>()
                .AddLogging(lb => lb.ClearProviders().AddSerilog())
                .BuildServiceProvider();

            await ActivatorUtilities.CreateInstance<Executor>(serviceProvider).Do();
        }
    }
}
