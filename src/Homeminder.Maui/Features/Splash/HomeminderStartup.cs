using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Airframe;

namespace Homeminder.Maui;

/// <summary>
/// Represents the application startup sequence.
/// </summary>
public class HomeminderStartup : IApplicationStartup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationStartup"/> class.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="startupTasks">The startup tasks.</param>
    public HomeminderStartup(ILoggerFactory loggerFactory, IEnumerable<IStartupOperation> startupTasks)
    {
        _logger = loggerFactory.CreateLogger(GetType());
        _startupTasks = startupTasks;
    }

    /// <inheritdoc/>
    public IObservable<Unit> Startup() => _startupTasks
        .Where(operation => operation.CanExecute())
        .Select(operation => operation.Start())
        .Merge()
        .Do(_ => { })
        .PublishLast()
        .RefCount()
        .Do(_ => { });

    private readonly ILogger _logger;
    private readonly IEnumerable<IStartupOperation> _startupTasks;
}