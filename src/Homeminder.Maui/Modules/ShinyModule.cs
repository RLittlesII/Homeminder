using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;
using Shiny.Stores;

namespace Homeminder.Maui;

public class ShinyModule : ServiceCollectionModule
{
    /// <inheritdoc />
    protected override IServiceCollection Load(IServiceCollection serviceCollection) =>
        serviceCollection
            .AddSingleton<IKeyValueStore, MemoryKeyValueStore>();
}