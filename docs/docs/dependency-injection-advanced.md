# Advanced DI

A Xamarin.Forms project can container main views and view models and registering all of these in the DI container can be a chore. Luckily using extension methods on the `IServiceCollection`, we can author code to simplify the registration process. 

Here we have extension methods that will attempt to register any type located within a specified assembly that matches the given type parameter.
```csharp
public static IServiceCollection AddAll<T>(this IServiceCollection services)
{
    return AddAll<T>(services, Assembly.GetExecutingAssembly());
}

public static IServiceCollection AddAll<T>(this IServiceCollection services, Assembly assembly)
{
    var definedTypes = assembly.DefinedTypes.ToList();

    foreach (var type in definedTypes)
    {
        if (!IsSameOrSubclass(type, typeof(T)))
        {
            continue;
        }

        if (!type.IsAbstract)
        {
            services.TryAddTransient(type);
        }
    }

    bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsAssignableFrom(potentialBase)
                || potentialDescendant == potentialBase;
    }

    return services;
}
```

Often view models will inherit some sort of base class or interface, we can then use that as a target for finding and registering them.

### Add views 
Here we add views into DI that inherit an `IViewModel` interface.
```csharp
public static IHostBuilder BuildHost() =>
    XamarinHost.CreateDefaultBuilder<App>()
    .ConfigureServices((context, services) => 
    {
    services.AddAll<IViewModel>();
    });
```

### Add pages 
In this example, we are adding anything that inherits from a Xamarin.Forms `Page`.
```csharp
public static IHostBuilder BuildHost() =>
    XamarinHost.CreateDefaultBuilder<App>()
    .ConfigureServices((context, services) => 
    {
        services.AddAll<Page>();
    });
```

Now all of your pages and view models are in the DI, calling them is easy:

### Consuming
Here we set the `App.MainPage` by obtaining page and view model from the DI container.
```csharp
public void OnStart()
{
    var viewModel = Host.Services.GetRequiredService<HomeViewModel>();
    var page = Host.Services.GetRequiredService<HomeView>();
    page.BindingContext = viewModel;

    MainPage = page;
}
```