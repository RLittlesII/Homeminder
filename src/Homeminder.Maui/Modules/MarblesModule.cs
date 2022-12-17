using System.Reactive.Concurrency;
using ReactiveMarbles.Locator;
using ReactiveMarbles.Mvvm;
using ReactiveUI;
using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;

namespace Homeminder.Maui;

public class MarblesModule : ServiceCollectionModule
{
    protected override IServiceCollection Load(IServiceCollection serviceCollection)
    {
        var coreRegistration = CoreRegistrationBuilder
            .Create()
            .WithMainThreadScheduler(RxApp.MainThreadScheduler)
            .WithTaskPoolScheduler(TaskPoolScheduler.Default)
            .WithExceptionHandler(new DebugExceptionHandler())
            .Build();

        ServiceLocator
            .Current()
            .AddCoreRegistrations(() => coreRegistration);

        return serviceCollection.AddSingleton<ICoreRegistration>(_ => coreRegistration);
    }
}