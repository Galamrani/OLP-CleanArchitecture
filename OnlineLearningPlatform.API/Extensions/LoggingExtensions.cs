using Serilog;

namespace OnlineLearningPlatform.API.Extensions;

public static class LoggingExtensions
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        string logFilePath = "Logs/api-.log";

        var logger = new LoggerConfiguration()
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 2)
            .MinimumLevel.Information()
            .CreateLogger();

        builder.Logging.AddSerilog(logger, dispose: true);
    }
}