using Problem.Infrastructure.Domain;
using Problem.Application;
using Problem.Application.Utility;

namespace Problem.Application
{
    /// <summary>
    /// The consumer service sleeps for a random duration between 0 and 100 milliseconds.
    /// After waking up, it retrieves an integer from a data queue and writes it to an output file called 'output.txt'.
    /// If the data queue is empty when a consumer attempts to access it, the consumer thread will wait until a new element is added to the queue by a producer.
    /// </summary>
    public class ConsumerService : BaseSyncService
    {
        /// <summary>
        /// The minimum size of the queue for reading data.
        /// </summary>
        private const int MIN_SIZE_OF_QUEUE = 0;

        /// <summary>
        /// The minimum limit of write waiting time.
        /// </summary>
        private const int MIN_LIMIT_OF_WRITE_WAITING = 80;

        /// <summary>
        /// Indicates whether writing has been stopped.
        /// </summary>
        private bool _stoppedWriting;

        /// <summary>
        /// The data reader
        /// </summary>
        private readonly IReadData _readData;

        /// <summary>
        /// The file writer
        /// </summary>
        private readonly IWriteData _fileWriter;

        /// <summary>
        /// The consumer event
        /// </summary>
        private readonly IConsumerEvent _consumerEvent;

        public ConsumerService(IReadData readData, IWriteData fileWrite, IConsumerEvent consumerEvent, CancellationToken cancellationToken)
            : base(new AutoResetEvent(false), cancellationToken)
        {
            _readData = readData;
            _fileWriter = fileWrite;
            _consumerEvent = consumerEvent;

            _consumerEvent.StartReadAction += OnCanReadAction;
            _consumerEvent.StopWriteAction += OnStopWriteAction;
        }

        /// <inheritdoc/>
        public override void Work()
        {
            while (IsAssessableToExecute())
            {
                ThreadWait();
                RandomThreadSleep();

                var count = _readData.Count();
                if (count == MIN_SIZE_OF_QUEUE)
                {
                    continue;
                }

                if (_stoppedWriting && count <= MIN_LIMIT_OF_WRITE_WAITING)
                {
                    var isWriting = _consumerEvent.StartWriteAction.Invoke();
                    _stoppedWriting = !isWriting;
                }

                var value = _readData.Get();
                SetThreadSignal();

                _fileWriter.Write(value);
            }
        }

        protected override bool IsAssessableToExecute()
        {
            return !_cancellationToken.IsCancellationRequested || _readData.Count() > 0;
        }

        /// <summary>
        /// Signals the consumer synchronization object that data can be read.
        /// </summary>
        private void OnCanReadAction()
        {
            SetThreadSignal();
        }

        /// <summary>
        /// Sets the flag indicating that writing has been stopped.
        /// </summary>
        private void OnStopWriteAction()
        {
            _stoppedWriting = true;
        }
    }
}
