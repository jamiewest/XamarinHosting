using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Allows consumers to perform cleanup during a graceful shutdown.
    /// </summary>
    public class XamarinApplicationLifetime : ApplicationLifetime, IXamarinApplicationLifetime
    {
        
        private readonly IDictionary<string, Action> _resumingSource = new Dictionary<string, Action>();
        private readonly ILogger _logger;

        public XamarinApplicationLifetime(ILogger<ApplicationLifetime> logger) : base(logger)
        {
            _logger = logger;
            ApplicationSleeping = new LifecycleRegister();
            ApplicationResuming = new LifecycleRegister();
        }

        public LifecycleRegister ApplicationSleeping { get; }

        public LifecycleRegister ApplicationResuming { get; }

        /// <summary>
        /// Signals the ApplicationSleeping event and blocks until it completes.
        /// </summary>
        public void NotifySleeping()
        {
            try
            {
                ApplicationSleeping.Notify();
            }
            catch (Exception ex)
            {

                _logger.ApplicationError(
                    LoggerEventIds.ApplicationSleepingException,
                    "An error occurred while starting the application",
                    ex);
            }
        }

        /// <summary>
        /// Signals the ApplicationResuming event and blocks until it completes.
        /// </summary>
        public void NotifyResuming()
        {
            try
            {
                ApplicationResuming.Notify();
            }
            catch (Exception ex)
            {
                _logger.ApplicationError(
                    LoggerEventIds.ApplicationResumingException,
                     "An error occurred resuming the application",
                     ex);
            }
        }
    }
}
