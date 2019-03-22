# Hosted Services

The `Microsoft.Extensions.Hosting` package contains the interface `IHostedService` and the base class `BackgroundService`. These start and stop when the `Host` does, but in Xamarin.Forms we need to be able to sleep and resume services. 

Here is the `IXamarinHostedService` interface that extends the `IHostedService`.
```csharp
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Defines methods for objects that are managed by the host.
    /// </summary>
    public interface IXamarinHostedService : IHostedService
    {
        /// <summary>
        /// Triggered when the application host is ready to sleep.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the sleep process has been aborted.</param>
        Task SleepAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Triggered when the application host is ready to resume.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the resume process has been aborted.</param>
        Task ResumeAsync(CancellationToken cancellationToken);
    }
}
```

The `App.xaml.cs` contians the OnSleep() and OnResume() methods that in turn call the Host.SleepAsync() and Host.Resume() methods respectively. These are then called on every registered HostedService and also on any registered callbacks set by the `XamarinHostApplicationLifetime` instance.

```csharp
protected override void OnSleep()
{
    Task.Run(async () => await Host.SleepAsync());
}

protected override void OnResume()
{
    Task.Run(async () => await Host.ResumeAsync());
}
```

Also included is the `XamarinBackgroundService` class that extends `BackgroundService`. This makes setting up a service that will execute code at startup and resume executing code when returning from a sleep state simple.

Here is an example service that will output some log messages every 5 seconds.
```csharp
public class SimpleBackgroundService : XamarinBackgroundService
{
    public SimpleBackgroundService(ILogger<SimpleBackgroundService> logger) => Logger = logger;

    public ILogger Logger { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("SimpleBackgroundService is starting...");

        stoppingToken.Register(() =>
            Logger.LogInformation($"SimpleBackgroundService is stopping..."));
        
        while (!stoppingToken.IsCancellationRequested)
        {
            Logger.LogInformation("SimpleBackgroundService is running...");
            await Task.Delay(5000, stoppingToken);
        }
    }
}
```

The `UseHostedService<T>` method can be called on the HostBuilder to simplify hosted service setup.

```csharp
public static IHostBuilder BuildHost() => 
        XamarinHost.CreateDefaultBuilder<App>()
        .UseHostedService<SimpleBackgroundService>();
```

It can also be added directly in the ConfigureServices section.

```csharp
public static IHostBuilder BuildHost() => 
        XamarinHost.CreateDefaultBuilder<App>()
        .ConfigureServices((context, services) => 
        {
            services.AddHostedService<SimpleBackgroundService>();
        });
```