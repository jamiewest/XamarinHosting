namespace Microsoft.Extensions.Hosting.Internal
{
    internal static class LoggerEventIds
    {
        public const int StoppedWithException = 5;
        public const int Sleeping = 9;
        public const int Resuming = 10;
        public const int ApplicationSleepingException = 11;
        public const int ApplicationResumingException = 12;
    }
}