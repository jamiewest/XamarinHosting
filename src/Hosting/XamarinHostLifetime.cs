using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Hosting
{
    public class XamarinHostLifetime : IHostLifetime, IDisposable
    {
        private CancellationTokenRegistration _applicationStartedRegistration;

        public XamarinHostLifetime(
            IOptions<XamarinHostLifetimeOptions> options, 
            IHostEnvironment environment, 
            IHostApplicationLifetime applicationLifetime)
            : this(options, environment, applicationLifetime, NullLoggerFactory.Instance) { }

        public XamarinHostLifetime(
            IOptions<XamarinHostLifetimeOptions> options, 
            IHostEnvironment environment, 
            IHostApplicationLifetime applicationLifetime, 
            ILoggerFactory loggerFactory)
        {
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Lifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            Logger = loggerFactory.CreateLogger("Microsoft.Extensions.Hosting.Host");
        }

        private IHostEnvironment Environment { get; }

        private IHostApplicationLifetime Lifetime { get; }

        private ILogger Logger { get; }

        private XamarinHostLifetimeOptions Options { get; }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            if (!Options.SuppressStatusMessages)
            {
                _applicationStartedRegistration = Lifetime.ApplicationStarted.Register(state =>
                {
                    ((XamarinHostLifetime)state).OnApplicationStarted();
                },
                this);
            }
            
            return Task.CompletedTask;
        }

        private void OnApplicationStarted()
        {
            Logger.LogInformation("Application started.");
            Logger.LogInformation("Hosting environment: {envName}", Environment.EnvironmentName);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _applicationStartedRegistration.Dispose();
        }
    }
}
