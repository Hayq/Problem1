using Problem.Application;

namespace Problem.Application
{
    public abstract class BaseSyncService : BaseService
    {
        /// <summary>
        /// The consumer synchronization object.
        /// </summary>
        private readonly AutoResetEvent _syncEvent;

        public BaseSyncService(AutoResetEvent syncEvent, CancellationToken cancellationToken)
            : base(cancellationToken)
        {
            _syncEvent = syncEvent;
        }

        /// <summary>
        /// Signal the consumer synchronization object to unlock waiting
        /// </summary>
        /// <returns>true if operation succeeds, otherwise, false</returns>
        protected bool SetThreadSignal()
        {
            var isSucceeded = false;

            if (IsAssessableToExecute())
            {
                isSucceeded = _syncEvent.Set();
            }

            return isSucceeded;
        }

        /// <summary>
        /// Signal the consumer synchronization object to lock waiting
        /// </summary>
        /// <returns>true if operation succeeds, otherwise, false</returns>
        protected bool ResetThreadSignal()
        {
            var isSucceeded = false;

            if (IsAssessableToExecute())
            {
                isSucceeded = _syncEvent.Reset();
            }

            return isSucceeded;
        }

        /// <summary>
        /// Wait in threads queue
        /// </summary>
        /// <returns>true if operation succeeds, otherwise, false</returns>
        protected bool ThreadWait()
        {
            return _syncEvent.WaitOne(THREAD_WAIT_EXPIRATION);
        }

        /// <summary>
        /// Represents accessibility to execute speciated work
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsAssessableToExecute()
        {
            return !_cancellationToken.IsCancellationRequested;
        }
    }
}
