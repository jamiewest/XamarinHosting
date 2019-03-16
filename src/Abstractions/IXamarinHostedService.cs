using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Defines methods for objects that are managed by the host.
    /// </summary>
    public interface IXamarinHostedService : IHostedService
    {
        /// <summary>
        /// Triggered when the application host is ready to sleep.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the sleep process has been aborted.</param>
        Task SleepAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Triggered when the application host is ready to resume.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the resume process has been aborted.</param>
        Task ResumeAsync(CancellationToken cancellationToken);
    }
}
