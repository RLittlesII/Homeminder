namespace Homeminder.Maui;

public static class NavigationUri
{
    // NOTE: [rlittlesii: December 18, 2022] Expactation is this works.
    // public static Uri MainNavigation => new Uri($"{Root}{NavigationPage}/{nameof(MainPage)}");

    // public static Uri MainNavigation => new($"{Root}{NavigationPage}/{nameof(MainPage)}");

    public static Uri MainNavigation => new($"/{NavigationPage}/{nameof(MainPage)}");

    public static Uri Splash => new($"/{NavigationPage}/{nameof(SplashScreen)}");

    public static string Back => "../";

    public static string Root => "//";

    public static string NavigationPage => nameof(NavigationPage);
}