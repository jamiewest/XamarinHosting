# Xamarin Forms Generic Host
[![Build status](https://dev.azure.com/jamiewest/XamarinFormsHost/_apis/build/status/XamarinFormsHost-CI)](https://dev.azure.com/jamiewest/XamarinFormsHost/_build/latest?definitionId=28)

A Xamarin Forms ```IHostLifetime``` implementation for `Microsoft.Extensions.Hosting`. 

## Simple Example Usage
```csharp
    var host = new HostBuilder()
        .UseXamarinFormsLifetime()
```

## Generic Host Example
```csharp
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DockerApp
{
    public partial class App : Application
    {
        public App() => InitializeComponent();

        public App(IHost host) : this() => Host = host;

        public static IHost Host { get; private set; }

        private XamarinApplicationLifetime Lifetime { get; set; }

        public static IHostBuilder BuildHost() =>
            new HostBuilder()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    hostContext.HostingEnvironment.EnvironmentName = "Development";
                    
                    // The EmbeddedFileProvider looks up files using embedded resources in the specified
                    // assembly. This file provider is case sensitive. For the .json files below, be sure
                    // set the Build Action to 'Embedded resource'
                    config.SetFileProvider(new EmbeddedFileProvider(Assembly.GetExecutingAssembly()));
                    config.AddJsonFile("appsettings.json", true);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",true;
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<App>();

                    // This will add the IStringLocalizer class and user the Resources folder in your project root.
                    // See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.2
                    services.AddLocalization(
                        options => options.ResourcesPath = "Resources");
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddDebug();
                })
                .UseXamarinFormsLifetime();

        protected override void OnStart()
        {
            Lifetime =
                Host.Services.GetRequiredService<IApplicationLifetime>() as XamarinApplicationLifetime;
            
            Host.Start();
        }

        protected override void OnSleep()
        {
            Lifetime.NotifySleeping();
        }

        protected override void OnResume()
        {
            Lifetime.NotifyResuming();
        }
    }
}
```

Required .csproj packages
```xml
<ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.FileProviders.Embedded" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.Localization" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="2.2.0" />
  </ItemGroup>
```

iOS AppDelegate.cs
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

> PM> Install-Package West.Extensions.Hosting.XamarinForms

**Visual Studio**
Right click to your project in Visual Studio, choose “Manage NuGet Packages” and search for ‘West.Extensions.Hosting.XamarinForms’ and click ‘Install’.
([see NuGet Gallery][nuget-gallery].)

**.NET Core Command Line Interface**
Run the following command from your favorite shell or terminal:

> dotnet add package West.Extensions.Hosting.XamarinForms
