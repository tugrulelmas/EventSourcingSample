using EventSourcingSample.UserCommandConsumer.Abstractions;
using EventSourcingSample.UserCommandConsumer.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace EventSourcingSample.UserCommandConsumer
{
    class Program
    {
        private static void Main(string[] args) {
            var serviceProvider = RegisterServices(args);

            var consumer = serviceProvider.GetService<IConsumer>();
            consumer.Consume();
        }

        private static ServiceProvider RegisterServices(string[] args) {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return new ServiceCollection()
            .AddOptions()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<IConsumer, UserCreateCommandConsumer>()
            .BuildServiceProvider();
        }
    }
}
