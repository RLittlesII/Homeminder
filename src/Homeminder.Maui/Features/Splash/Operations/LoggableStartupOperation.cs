using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Airframe;

namespace Homeminder.Maui;

public abstract class LoggableStartupOperation : IStartupOperation
{
    protected LoggableStartupOperation(ILoggerFactory factory) => Logger = factory.CreateLogger(GetType());

    /// <inheritdoc/>
    IObservable<Unit> IStartupOperation.Start() =>

        // Add logging.
        Start().Finally(() => Logger.LogTrace("Completed {Operation}", GetType().Name));

    /// <inheritdoc/>
    bool IStartupOperation.CanExecute()
    {
        var canExecute = CanExecute();
        Logger.LogTrace("Can Execute: {CanExecute}", canExecute);
        return canExecute;
    }

    /// <summary>
    /// Template method for the startup operation.
    /// </summary>
    /// <returns>A completion notification.</returns>
    protected abstract IObservable<Unit> Start();

    /// <summary>
    /// Template method for whether or not the startup operation will execute.
    /// </summary>
    /// <returns>A completion notification.</returns>
    protected virtual bool CanExecute() => true;

    protected ILogger Logger { get; }
}