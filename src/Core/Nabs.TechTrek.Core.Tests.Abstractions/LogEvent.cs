using Microsoft.Extensions.Logging;

namespace Nabs.TechTrek.Core.Tests.Abstractions;

public class LogEvent
{
    public DateTime Timestamp { get; }
    public LogLevel LogLevel { get; }
    public string Category { get; }
    public EventId EventId { get; }
    public string Message { get; }
    public Exception Exception { get; }

    public LogEvent(DateTime timestamp, LogLevel logLevel, string category, EventId eventId, string message, Exception exception)
    {
        Timestamp = timestamp;
        LogLevel = logLevel;
        Category = category;
        EventId = eventId;
        Message = message;
        Exception = exception;
    }
}