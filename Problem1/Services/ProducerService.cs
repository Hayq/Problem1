using Problem1.Models;

namespace Problem1.Services
{
    public class ProducerService : BaseService
    {
        private const int MAX_SIZE_OF_QUEUE = 100;

        private readonly IReadWriteData _readWriteData;
        private readonly IProducerEvent _producerAction;
        private readonly CancellationToken _cancellationToken;
        private readonly AutoResetEvent _producerSync;

        public ProducerService(IReadWriteData writeData, IProducerEvent producerAction, CancellationToken cancellationToken)
        {
            _readWriteData = writeData;
            _producerAction = producerAction;
            _cancellationToken = cancellationToken.Register(OnCancel).Token;

            _producerSync = new AutoResetEvent(true);
            producerAction.StartWriteAction = OnStartWriteAction;
        }

        public void ProducerWork()
        {
            var producerId = Thread.CurrentThread.Name;

            while (!_cancellationToken.IsCancellationRequested)
            {
                _producerSync.WaitOne();

                RandomThreadSleep();

                //lock (_readWriteData.LockObject)
                //{
                var count = _readWriteData.Count();
                if (count == MAX_SIZE_OF_QUEUE)
                {
                    _producerAction.StopWriteAction?.Invoke();
                    continue;
                }

                var value = _random.Next(int.MinValue, int.MaxValue);
                _readWriteData.Write(value);

                if (count == 0)
                {
                    _producerAction.StartReadAction?.Invoke();
                }

                _producerSync.Set();
                //}
            }
        }

        private bool OnStartWriteAction()
        {
            return _producerSync.Set();
        }

        private void OnCancel()
        {
            _producerAction.StartReadAction?.Invoke();
            _producerSync.Reset();
        }
    }
}
