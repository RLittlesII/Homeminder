using Prism.Navigation;

namespace Homeminder.Maui;

public static class INavigationServiceExtensions
{
    public static Task HandleResult(this Task<INavigationResult> task) => HandleResult(task, result => Console.WriteLine(result.Exception));

    public static Task HandleResult(this Task<INavigationResult> task, Action<INavigationResult> handler) => task.ContinueWith(
        continuation =>
        {
            if (!continuation.Result.Success)
            {
                handler.Invoke(continuation.Result);
            }
        }
    );
}