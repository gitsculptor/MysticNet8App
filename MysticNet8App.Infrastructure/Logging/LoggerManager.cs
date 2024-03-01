using MysticNet8App.Contracts.Interfaces;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace MysticNet8App.Infrastructure.Logging;

public class LoggerManager : ILoggerManager
{
    private static readonly Logger Logger  = new LoggerConfiguration()
        // add console as logging target
        .WriteTo.Console()
        // add a logging target for warnings and higher severity  logs
        // structured in JSON format
        .WriteTo.File(new JsonFormatter(),
            "important.json",
            restrictedToMinimumLevel: LogEventLevel.Warning)
        // add a rolling file for all logs
        .WriteTo.File("all-.logs",
            rollingInterval: RollingInterval.Day)
        // set default minimum level
        .MinimumLevel.Debug()
        .CreateLogger();
    
    public void LogInfo(string message)
    {
        Logger.Information(message);
    }

    public void LogWarn(string message)
    {
        Logger.Warning(message);
    }

    public void LogDebug(string message)
    {
        Logger.Debug(message);
    }

    public void LogError(string message)
    {
        Logger.Error(message);
    }
}