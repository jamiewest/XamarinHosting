using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseXamarinFormsLifetime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, collection) => collection.AddSingleton<IHostLifetime, XamarinFormsLifetime>());
        }

        public static IHostBuilder UseXamarinFormsLifetime(this IHostBuilder hostBuilder, Action<XamarinFormsLifetimeOptions> configureOptions)
        {
            return hostBuilder.ConfigureServices((context, collection) =>
            {
                collection.AddSingleton<IHostLifetime, XamarinFormsLifetime>();
                collection.Configure(configureOptions);
            });
        }
    }
}
