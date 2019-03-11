namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Allows consumers to perform cleanup during application sleep/resume.
    /// </summary>
    public interface IXamarinLifetime : IHostApplicationLifetime
    {
        /// <summary>
        /// Triggered when the application has gone to sleep.
        /// </summary>
        LifecycleRegister ApplicationSleeping { get; }

        /// <summary>
        /// Triggered when the application has resumed.
        /// </summary>
        LifecycleRegister ApplicationResuming { get; }
    }
}