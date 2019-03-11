using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Hosting
{
    public static class XamarinHost
    {
        public static IHostBuilder CreateDefaultBuilder()
        {
            var builder = new HostBuilder();

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.SetFileProvider(new EmbeddedFileProvider(Assembly.GetExecutingAssembly()));
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
            .UseXamarinLifetime(); 

            return builder;
        }
    }
}
