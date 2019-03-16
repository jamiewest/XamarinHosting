using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extends the <see cref="IHost"/>.
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// Puts the host to sleep synchronously.
        /// </summary>
        /// <param name="host">The <see cref="IHost"/> that is being extended.</param>
        public static void Sleep(this IHost host)
        {
            host.SleepAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Signals that the <see cref="IHost"/> will be sleeping.
        /// </summary>
        /// <param name="host">The <see cref="IHost"/> that is being extended.</param>
        public static async Task SleepAsync(this IHost host, CancellationToken cancellationToken = default)
        {
            var hostedServices = host.Services.GetService<IEnumerable<IHostedService>>();
            foreach (var hostedService in hostedServices)
            {
                // Fire IXamarinHostedService.Sleep
                if (hostedService is IXamarinHostedService service)
                {
                    await service.SleepAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            
            var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>() as XamarinHostApplicationLifetime;
            lifetime?.NotifySleeping();

            var logger = host.Services.GetRequiredService<ILogger<IHost>>();
            logger.Sleeping();
        }

        // <summary>
        /// Resumes the host synchronously.
        /// </summary>
        /// <param name="host">The <see cref="IHost"/> that is being extended.</param>
        public static void Resume(this IHost host)
        {
            host.ResumeAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Signals that the <see cref="IHost"/> will be resuming.
        /// </summary>
        /// <param name="host">The <see cref="IHost"/> that is being extended.</param>
        public static async Task ResumeAsync(this IHost host, CancellationToken cancellationToken = default)
        {
            var hostedServices = host.Services.GetService<IEnumerable<IHostedService>>();
            foreach (var hostedService in hostedServices)
            {
                // Fire IXamarinHostedService.Sleep
                if (hostedService is IXamarinHostedService service)
                {
                    await service.ResumeAsync(cancellationToken).ConfigureAwait(false);
                }
            }

            var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>() as XamarinHostApplicationLifetime;
            lifetime?.NotifyResuming();

            var logger = host.Services.GetRequiredService<ILogger<IHost>>();
            logger.Resuming();
        }
    }
}
