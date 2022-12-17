using Microsoft.Extensions.Logging;

namespace Homeminder.Maui;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }
}