# Configuration

Application configuration can be achieved using key-value pairs established by configuration providers. Using Xamarin.Forms you will most likely use Settings files, In-memory, or potentially a custom provider. 

## Default Builder

The `XamarinHost.CreateDefaultBuilder<TAppplication>()` method includes setup of both **appsettings.json** and **appsettings.{EnvinromentName}.json** files using an [EmbeddedFileProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.fileproviders.embeddedfileprovider). The `EmbeddedFileProvider` will look in the same assembly where the `TApplication` class is located. 

> Both the **appsettings.json** and **appsettings.{EnvinromentName}.json** will need to the Build Action set to *Embedded resource* in Visual Studio in order to accessible to the `EmbeddedFileProvider`.

This is what the definition looks like when calling from the Xamarin.Forms `App` class.
```csharp
public static IHostBuilder BuildHost() =>
    XamarinHost.CreateDefaultBuilder<App>();
```

Here is a look at what happens with configuration when the default builder is called.
```csharp
public static IHostBuilder CreateDefaultBuilder<TApplication>() where TApplication : class
{
    ...
            
    builder.ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;
        
        config.SetFileProvider(new EmbeddedFileProvider(typeof(TApplication).Assembly));
        config.AddJsonFile("appsettings.json", true);
        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
    })
    
    ...

    return builder;
}
```

