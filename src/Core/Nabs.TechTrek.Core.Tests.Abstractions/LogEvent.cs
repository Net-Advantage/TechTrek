using Microsoft.Extensions.Logging;

namespace Nabs.TechTrek.Core.Tests.Abstractions;

public record LogEvent(
    DateTime Timestamp, 
    LogLevel LogLevel, 
    string Category, 
    EventId EventId, 
    string Message, 
    Exception Exception);