# Localization



```csharp
public static IHostBuilder BuildHost() =>
    XamarinHost.CreateDefaultBuilder<App>()
    .ConfigureServices((context, services) => 
    {
        services.AddLocalization(options => options.Resources == "Resources");
    });
```