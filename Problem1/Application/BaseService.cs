namespace Problem.Application
{
    /// <summary>
    /// Represents an abstract base class for services
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Thread waiting expiration time in milliseconds.
        /// </summary>
        public const int THREAD_WAIT_EXPIRATION = 1000;

        /// <summary>
        /// Random value generator
        /// </summary>
        protected static readonly Random _random = new Random();

        /// <summary>
        /// The cancellation token for the service.
        /// </summary>
        protected readonly CancellationToken _cancellationToken;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="cancellationToken"></param>
        public BaseService(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        /// <summary>
        /// Generates a random sleep time between 0 and 100 milliseconds and suspends the current thread for that duration.
        /// </summary>
        protected virtual void RandomThreadSleep()
        {
            Thread.Sleep(_random.Next(101));
        }

        /// <summary>
        /// Represents the core functionality for specified service
        /// </summary>
        public abstract void Work();
    }
}
