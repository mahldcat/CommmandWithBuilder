using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommandLine.Service;

public class CommandLineAppService(ILogger<CommandLineAppService> logger, IHostApplicationLifetime hostApplicationLifetime) : IHostedService
{
    private async Task CommandLineTask(CancellationToken cancellationToken)
    {
        logger.LogInformation("CommandLineTask Started");
        await Task.Delay(2000,cancellationToken);
        logger.LogInformation("CommandLineTask Completed");
    }
    
    #region IHostedService
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("StartAsync Started...");
        try
        {
            await CommandLineTask(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in Execution of Task");
        }
        finally
        {
            hostApplicationLifetime.StopApplication();
        }
        logger.LogInformation("StartAsync Completed...");
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("StopAsync");
        return Task.CompletedTask;
    }
    #endregion
}
