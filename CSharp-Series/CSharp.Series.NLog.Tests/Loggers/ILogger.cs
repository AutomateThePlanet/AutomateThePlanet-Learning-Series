using System;

namespace CSharp.Series.NLog.Tests.Loggers
{
public interface ILogger
{
    void LogInfo(string message);

    void LogError(Exception exception);
}
}