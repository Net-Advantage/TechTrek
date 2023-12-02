using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Nabs.TechTrek.Core.Tests.Abstractions;

public sealed class XUnitTestOutputLoggerProvider(
    ITestOutputHelper testOutputHelper) 
    : ILoggerProvider
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

    public ILogger CreateLogger(string categoryName)
    {
        return new XUnitTestOutputLogger(_testOutputHelper);
    }

    public void Dispose()
    {
    }

    private class XUnitTestOutputLogger(
        ITestOutputHelper testOutputHelper) 
        : ILogger
    {
        private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;
        private readonly List<LogEvent> _logEvents = [];

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            _testOutputHelper.WriteLine($"{logLevel}: {formatter(state, exception)}");

            if (exception is null)
            {
                return;
            }
            
            _testOutputHelper.WriteLine(exception.ToString());

            var logEvent = new LogEvent(
                               DateTime.Now,
                                              logLevel,
                                              string.Empty,
                                              eventId,
                                              formatter(state, exception),
                                              exception);
            _logEvents.Add(logEvent);
        }
    }
}
