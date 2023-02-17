using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;
using ReactiveMarbles.Mvvm;
using ReactiveUI;

namespace Homeminder.Maui;

using System.ComponentModel;

[SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Disposed with Garbage.")]
public class ViewModelBase : RxDisposableObject, INavigatedAware, IDestructible, IReactiveObject
{
    protected ViewModelBase(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
        _navigatedTo = new Subject<INavigationParameters>().DisposeWith(Garbage);
        _navigatedFrom = new Subject<INavigationParameters>().DisposeWith(Garbage);
    }

    protected CompositeDisposable Garbage { get; } = new();

    protected ILogger Logger { get; }

    protected IObservable<INavigationParameters> NavigatedFrom => _navigatedFrom.AsObservable().Publish().RefCount();

    protected IObservable<INavigationParameters> NavigatedTo => _navigatedTo.AsObservable().Publish().RefCount();

    /// <inheritdoc />
    void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters) => _navigatedFrom.OnNext(parameters);

    /// <inheritdoc />
    void INavigatedAware.OnNavigatedTo(INavigationParameters parameters) => _navigatedTo.OnNext(parameters);

    /// <inheritdoc />
    void IDestructible.Destroy() => Dispose(true);

    /// <inheritdoc />
    void IReactiveObject.RaisePropertyChanging(PropertyChangingEventArgs args) => RaisePropertyChanging(args.PropertyName ?? throw new InvalidOperationException());

    /// <inheritdoc />
    void IReactiveObject.RaisePropertyChanged(PropertyChangedEventArgs args) => RaisePropertyChanged(args.PropertyName ?? throw new InvalidOperationException());

    /// <inheritdoc />
    protected override void Dispose(bool disposing) => Garbage.Dispose();

    private readonly Subject<INavigationParameters> _navigatedTo;
    private readonly Subject<INavigationParameters> _navigatedFrom;
}