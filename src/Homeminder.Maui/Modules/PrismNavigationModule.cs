using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;

namespace Homeminder.Maui;

public class PrismNavigationModule : ServiceCollectionModule
{
    /// <inheritdoc />
    protected override IServiceCollection Load(IServiceCollection serviceCollection)
    {
        // serviceCollection.UseGps<Gps>();
        return serviceCollection
            .RegisterForNavigation<NavigationPage>(nameof(NavigationPage))
            .RegisterForNavigation<MainPage, MainViewModel>(nameof(MainPage));
    }
}