using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseHostedService<T>(this IHostBuilder hostBuilder)
            where T : class, IHostedService, IDisposable => 
            hostBuilder.ConfigureServices(services =>
                services.AddHostedService<T>());

        public static IHostBuilder UseXamarinLifetime(this IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureServices((context, collection) =>
            {
                collection.AddSingleton<IHostApplicationLifetime, XamarinLifetime>();
                collection.AddSingleton<IHostLifetime, XamarinHostLifetime>();
            });

        public static IHostBuilder UseXamarinLifetime(this IHostBuilder hostBuilder, Action<XamarinHostLifetime> configureOptions) =>
            hostBuilder.ConfigureServices((context, collection) =>
            {
                UseXamarinLifetime(hostBuilder);
                collection.Configure(configureOptions);
            });
    }
}
