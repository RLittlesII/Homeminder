using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;

namespace Homeminder.Maui;

public class AirframeModule : ServiceCollectionModule
{
    /// <inheritdoc />
    protected override IServiceCollection Load(IServiceCollection serviceCollection) => serviceCollection
        // .AddSingleton<ISchedulerProvider, SchedulerProvider>()
        // .AddSingleton<ISettingsProvider, Setup.Settings>()
        .AddStartup<HomeminderStartup>(startupOption =>
            startupOption
                .AddOperation<DelayOperation>());
}