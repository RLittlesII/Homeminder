namespace Homeminder.Maui;

public static class NavigationUri
{
    public static Uri MainNavigation => new Uri($"{Root}{NavigationPage}/{nameof(MainPage)}");

    public static Uri Splash => new Uri($"/{NavigationPage}/{nameof(SplashScreen)}");

    public static string Back => "../";

    public static string Root => "//";

    public static string NavigationPage => nameof(NavigationPage);
}