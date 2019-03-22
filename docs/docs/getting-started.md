# Getting Started

This article will walk through the steps required to install and use the Generic Host from within a Xamarin.Forms application.

## Installation

You can add this library to your project using Nuget.

**Package Manager Console**
Run the following command in the “Package Manager Console”:

> PM> Install-Package West.Extensions.XamarinHosting

**Visual Studio**
Right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘West.Extensions.XamarinHosting’ and click ‘Install’.

## Setup 

Now that the package has been installed, we will need to update how the Xamarin.Forms `App` class is initialized on each platform.

### App

Decorate the `App.xaml.cs` class like the example below:

```csharp
public partial class App : Application
{
    public App() => InitializeComponent();

    public App(IHost host) : this() => Host = host;

    public static IHost Host { get; private set; }

    public static IHostBuilder BuildHost() => 
        XamarinHost.CreateDefaultBuilder<App>()
        .ConfigureServices((context, services) => 
        {
            services.AddScoped<MainPage>();
        });

    protected override void OnStart()
    {
        Task.Run(async () => await Host.StartAsync());
        MainPage = Host.Services.GetRequiredService<MainPage>();
    }

    protected override void OnSleep()
    {
        Task.Run(async () => await Host.SleepAsync());
    }

    protected override void OnResume()
    {
        Task.Run(async () => await Host.ResumeAsync());
    }
}
```

The static BuildHost() method will be called from each platform and returns the registered `App` instance that gets loaded. When the `App` instance is created, it receives the `IHost` from constructor injection and stores it as a static reference. This way the `IHost` is accessible throughout the Xamarin.Forms project along with it's services via the `Host.Services` property.

### Android

Decorate the Android `MainActivity.cs` like below:
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    ...

    var host = App.BuildHost()
        .UseContentRoot(System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal)).Build();

    var application = host.Services.GetRequiredService<App>();

    LoadApplication(application);

    ...
}
```

With Android we need to set the Content Root otherwise there is an error at startup.

### iOS

Decorate the iOS `AppDelegate.cs` like below:
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    ...
    var host = App.BuildHost().Build();

    var application = host.Services.GetRequiredService<App>();

    LoadApplication(application);

    ...
}
```