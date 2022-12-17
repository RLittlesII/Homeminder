using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using ReactiveMarbles.Mvvm;

namespace Homeminder.Maui;

public sealed class DelayOperation : LoggableStartupOperation
{
    public DelayOperation(ICoreRegistration coreRegistration, ILoggerFactory factory) : base(factory) => _coreRegistration = coreRegistration;

    protected override IObservable<Unit> Start() => Observable
        .Return(Unit.Default);

    private readonly ICoreRegistration _coreRegistration;
}