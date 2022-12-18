using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using Prism.Navigation;
using ReactiveMarbles.Mvvm;
using ReactiveUI;
using Rocket.Surgery.Airframe;

namespace Homeminder.Maui;

public class SplashViewModel : ViewModelBase
{
    public SplashViewModel(INavigationService navigationService,
        IApplicationStartup applicationStartup,
        ICoreRegistration coreRegistration,
        ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        Navigate = ReactiveCommand.Create(() => ExecuteNavigateWithUri(navigationService), outputScheduler: coreRegistration.MainThreadScheduler);
        Startup = ReactiveCommand.CreateFromObservable<INavigationParameters, Unit>(_ => applicationStartup.Startup().Do(_ => { }), outputScheduler: coreRegistration.MainThreadScheduler);

        Startup
            .IsExecuting
            .Skip(1)
            .DistinctUntilChanged()
            .ToProperty(this, nameof(IsBusy), out _isBusy, () => true, true, coreRegistration.MainThreadScheduler);

        NavigatedTo
            .InvokeCommand(this, viewModel => viewModel.Startup);

        Startup
            .Delay(TimeSpan.FromSeconds(3))
            .ObserveOn(coreRegistration.MainThreadScheduler)
            .InvokeCommand(this, viewModel => viewModel.Navigate);

        void ExecuteNavigateWithBuilder(INavigationService navigation) =>
            navigation
                .CreateBuilder()
                .AddSegment("MainPage")
                .AddNavigationPage()
                .Navigate();

        void ExecuteNavigateWithUri(INavigationService navigation) =>
            navigation
                .NavigateAsync(NavigationUri.MainNavigation)
                .HandleResult();
    }

    public ReactiveCommand<INavigationParameters,Unit> Startup { get; set; }

    public ReactiveCommand<Unit,Unit> Navigate { get; set; }

    public bool IsBusy => _isBusy.Value;

    private readonly ObservableAsPropertyHelper<bool> _isBusy;
}