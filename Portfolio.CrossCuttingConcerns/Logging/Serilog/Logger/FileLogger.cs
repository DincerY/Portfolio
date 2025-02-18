using Serilog;

namespace Portfolio.CrossCuttingConcerns.Logging.Serilog.Logger;

public class FileLogger : LoggerServiceBase
{
    public FileLogger()
    {
        string logFilePath = string.Format(format: "{0}{1}",
            arg0: Directory.GetCurrentDirectory() + "/logs/", arg1: ".txt");

        Logger = new LoggerConfiguration()
            .WriteTo.File(
                logFilePath, rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 5000000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}