# Xamarin.Forms Generic Host
[![Build status](https://dev.azure.com/jamiewest/XamarinHosting/_apis/build/status/XamarinHosting-CI)](https://dev.azure.com/jamiewest/XamarinHosting/_build/latest?definitionId=28)

A Xamarin.Forms generic host implementation for `Microsoft.Extensions.Hosting`. 

## Setup
Decorate the ```App``` class like this:
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
    TabLayoutResource = Resource.Layout.Tabbar;
    ToolbarResource = Resource.Layout.Toolbar;

    base.OnCreate(savedInstanceState);

    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

    var host = App.BuildHost()
        .UseContentRoot(System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal)).Build();

    var application = host.Services.GetRequiredService<App>();

    LoadApplication(application);
}
```

iOS `AppDelegate.cs`
```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();
    var host = App.BuildHost().Build();

    var application = host.Services.GetRequiredService<App>();

    LoadApplication(application);

    return base.FinishedLaunching(app, options);
}
```

## Installation

You can add this library to your project using [NuGet][nuget].

**Package Manager Console**
Run the following command in the “Package Manager Console”:

> PM> Install-Package West.Extensions.XamarinHosting

**Visual Studio**
Right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘West.Extensions.XamarinHosting’ and click ‘Install’.
([see NuGet Gallery][nuget-gallery].)

**.NET Core Command Line Interface**
Run the following command from your favorite shell or terminal:

> dotnet add package West.Extensions.XamarinHosting
