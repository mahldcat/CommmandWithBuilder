using CommandLine.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace CommandLine.Driver;

public class Driver
{
    private const string DOTNET_ENV = "DOTNET_ENVIRONMENT";
    private const string DEFAULT_ENV = "Development";

    private static string InitializeEnvironment()
    {
        string environment = Environment.GetEnvironmentVariable(DOTNET_ENV) ??
                             DEFAULT_ENV;
        Environment.SetEnvironmentVariable(DOTNET_ENV,environment);

        return environment;
    }

    
    //private static 
    public static async Task Main(string[] args)
    {
        InitializeEnvironment();

        IHost host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment.EnvironmentName;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<CommandLineAppService>();
                /*
                var env = hostContext.HostingEnvironment.EnvironmentName;
                if (env == Environments.Development)
                {
                }
                else if (env == "Test")
                {
                }
                else if (env == Environments.Production)
                {
                }
                */
            });
    
}