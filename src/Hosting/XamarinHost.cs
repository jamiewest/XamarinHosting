using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Provides convenience methods for creating instances of <see cref="IHost>"/> and <see cref="IHostBuilder>"/> with pre-configured defaults.
    /// </summary>
    public static class XamarinHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IHostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <typeparam name="TApplication">The type contianing the static <see cref="IHost"/> method.</typeparam>
        /// <returns>The initialized <see cref="IHostBuilder>"/>.</returns>
        public static IHostBuilder CreateDefaultBuilder<TApplication>() where TApplication : class
        {
            var builder = new HostBuilder();
                    
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.SetFileProvider(new EmbeddedFileProvider(typeof(TApplication).Assembly));
                config.AddJsonFile("appsettings.json", true);
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddDebug();
            })
            .UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            })
            .UseXamarinLifetime<TApplication>(); 

            return builder;
        }
    }
}
