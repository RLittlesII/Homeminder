using Rocket.Surgery.Airframe;
using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;

namespace Homeminder.Maui;

public class AirframeModule : ServiceCollectionModule
{
    /// <inheritdoc />
    protected override IServiceCollection Load(IServiceCollection serviceCollection) =>
        serviceCollection
            .AddStartup<ApplicationStartup>(startupOption =>
                                                startupOption
                                                    .AddOperation<DelayOperation>());
}