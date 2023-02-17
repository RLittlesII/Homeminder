using Microsoft.Extensions.Logging;
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
            .UsePrism(new DryIocContainerExtension(),
                prismAppBuilder => prismAppBuilder
                    .ConfigureServices(services =>
                        services
                            .AddModule<AirframeModule>()
                            .AddModule<ShinyModule>()
                            .AddModule<PrismNavigationModule>()
                            .AddModule<MarblesModule>()
                            .AddModule<SplashModule>()
                            .RegisterGlobalNavigationObserver()
                            .AddLogging(configure => configure.AddConsole()))
                    .OnAppStart((_, navigation) => navigation.NavigateAsync(NavigationUri.Splash).HandleResult())
                    .AddGlobalNavigationObserver((provider, context) => context.Subscribe(HandleNavigationRequestContext(provider))))
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }

    private static Action<NavigationRequestContext> HandleNavigationRequestContext(IContainerProvider provider) =>
        navigationRequestContext =>
        {
            var logger = provider.Resolve<ILogger<NavigationRequestContext>>();

            if (navigationRequestContext.Type == NavigationRequestType.Navigate)
            {
                logger.LogInformation("Navigation: {Uri}", navigationRequestContext.Uri);
            }

            else
            {
                logger.LogInformation("Navigation: {RequestType}", navigationRequestContext.Type);
            }

            var status = navigationRequestContext.Cancelled
                             ? "Cancelled"
                             : navigationRequestContext.Result.Success
                                 ? "Success"
                                 : "Failed";
            logger.LogInformation("Result: {Status}", status);

            if (status != "Failed"
             || string.IsNullOrEmpty(navigationRequestContext.Result?.Exception?.Message))
                return;

            var exception = navigationRequestContext.Result.Exception;
            logger.LogError(exception, exception.Message);
        };
}