namespace Portfolio.Web.Features.Telemetry;

/// <summary>
/// Source-generated, allocation-free log messages for
/// <see cref="PageVisitedLoggingHandler"/>.
/// </summary>
public sealed partial class PageVisitedLoggingHandler
{
    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Information,
        Message = "Page visited: {PageName}")]
    private static partial void LogPageVisited(ILogger logger, string pageName);
}
