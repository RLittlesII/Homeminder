using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
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
        Navigate = ReactiveCommand.CreateFromTask(token => ExecuteNavigate(navigationService), outputScheduler: coreRegistration.MainThreadScheduler);
        Startup = ReactiveCommand.CreateFromObservable<INavigationParameters, Unit>(_ => applicationStartup.Startup().Do(_ => { }), outputScheduler: coreRegistration.MainThreadScheduler);

        Startup
            .IsExecuting
            .DistinctUntilChanged()
            .ToProperty(this, nameof(IsBusy), out _isBusy, () => true, true, coreRegistration.MainThreadScheduler);

        NavigatedTo
            .InvokeCommand(this, viewModel => viewModel.Startup);

        Startup
            .ObserveOn(coreRegistration.MainThreadScheduler)
            .InvokeCommand(this, viewModel => viewModel.Navigate);

        Task ExecuteNavigate(INavigationService navigation) => navigation.NavigateAsync(NavigationUri.MainNavigation).HandleResult();
    }


    public ReactiveCommand<INavigationParameters,Unit> Startup { get; set; }

    public ReactiveCommand<Unit,Unit> Navigate { get; set; }

    public bool IsBusy => _isBusy.Value;

    private ObservableAsPropertyHelper<bool> _isBusy;
}