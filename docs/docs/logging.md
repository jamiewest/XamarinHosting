# Logging

## Default Builder
```csharp
public static IHostBuilder CreateDefaultBuilder<TApplication>() where TApplication : class
{
    var builder = new HostBuilder();
            
    ...

    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddDebug();
    })
    
    ...

    return builder;
}
```