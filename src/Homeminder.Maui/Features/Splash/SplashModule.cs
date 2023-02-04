using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;

namespace Homeminder.Maui;

public class SplashModule : ServiceCollectionModule
{
    protected override IServiceCollection Load(IServiceCollection serviceCollection) =>
        serviceCollection
            .RegisterForNavigation<SplashScreen, SplashViewModel>(nameof(SplashScreen));
}