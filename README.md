This archive is not actively maintained and has become outdated, I will try to update when I am able. Please also see the following reasources regarding using a `Generic Host` in Xamarin Forms.

[Add ASP.NET Core's Dependency Injection into Xamarin Apps with HostBuilder](https://montemagno.com/add-asp-net-cores-dependency-injection-into-xamarin-apps-with-hostbuilder/)

[Another example from James](https://github.com/jamesmontemagno/AllExtensions-DI-IoC/blob/master/AllExtensions/AllExtensions/Startup.cs)

# Xamarin.Forms Generic Host
[![Build status](https://dev.azure.com/jamiewest/XamarinHosting/_apis/build/status/XamarinHosting-CI)](https://dev.azure.com/jamiewest/XamarinHosting/_build/latest?definitionId=28)

A Xamarin.Forms generic host implementation for `Microsoft.Extensions.Hosting`. 

## Installation

You can add this library to your project using [NuGet](https://www.nuget.org/packages/West.Extensions.XamarinHosting/).

**Package Manager Console**
Run the following command in the “Package Manager Console”:

> PM> Install-Package West.Extensions.XamarinHosting

**Visual Studio**
Right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘West.Extensions.XamarinHosting’ and click ‘Install’.

**.NET Core Command Line Interface**
Run the following command from your favorite shell or terminal:

> dotnet add package West.Extensions.XamarinHosting

## Usage

Create a new Xamarin.Forms project and modify the following files to look like the examples below:

`App.xaml.cs`
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

Android `MainActivity.cs`
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    ...

    // Android requires that we set content root.
    var host = App.BuildHost()
        .UseContentRoot(System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal)).Build();

    var application = host.Services.GetRequiredService<App>();

    LoadApplication(application);

    ...
}
```

iOS `AppDelegate.cs`
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

## Documentation

Docs are a work in progress, they can be found [here](https://jamiewest.github.io/XamarinHosting/).
