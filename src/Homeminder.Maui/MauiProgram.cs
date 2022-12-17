using Prism.DryIoc;
using Rocket.Surgery.Airframe.Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace Homeminder.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism( new DryIocContainerExtension(),
                prismAppBuilder => prismAppBuilder
                    .ConfigureServices(services =>
                        services
                            .AddModule<AirframeModule>()
                            .AddModule<ShinyModule>()
                            .AddModule<PrismNavigationModule>()
                            .AddModule<MarblesModule>()
                            .AddLogging(configure => configure.AddConsole()))
                    .OnAppStart((_, navigation) => navigation.NavigateAsync(NavigationUri.Splash).HandleResult()))
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}