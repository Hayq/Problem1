using Problem.Infrastructure.Domain;
using Problem.Application;
using Problem.Application.Utility;

namespace Problem.Application
{
    /// <summary>
    ///The service running as separate threads. Sleeps for a random duration (0-100 ms), generates a random integer, and adds it to a shared data queue.If the queue reaches its maximum limit of 100 elements, the producer waits until the queue size decreases to 80 or fewer.
    /// </summary>
    public class ProducerService : BaseSyncService
    {
        private const int MAX_SIZE_OF_QUEUE = 99;

        private readonly IReadWriteData _readWriteData;
        private readonly IProducerEvent _producerAction;

        public ProducerService(IReadWriteData readWriteDate, IProducerEvent producerEvent, CancellationToken cancellationToken)
            : base(new AutoResetEvent(true), cancellationToken)
        {
            _readWriteData = readWriteDate;
            _producerAction = producerEvent;

            cancellationToken = cancellationToken.Register(OnCancel).Token;

            producerEvent.StartWriteAction += OnStartWriteAction;
        }

        /// <inheritdoc/>
        public override void Work()
        {
            while (IsAssessableToExecute())
            {
                ThreadWait();
                RandomThreadSleep();

                var count = _readWriteData.Count();
                if (count == MAX_SIZE_OF_QUEUE)
                {
                    _producerAction.StopWriteAction.Invoke();
                    continue;
                }

                Write();

                if (count == 0)
                {
                    _producerAction.StartReadAction.Invoke();
                }

                SetThreadSignal();
            }
        }

        private void Write()
        {
            if (IsAssessableToExecute())
            {
                var value = _random.Next(int.MinValue, int.MaxValue);
                _readWriteData.Write(value);
            }
        }

        private bool OnStartWriteAction()
        {
            return SetThreadSignal();
        }

        private void OnCancel()
        {
            _producerAction.StartReadAction.Invoke();
            ResetThreadSignal();
        }
    }
}
