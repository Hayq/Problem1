using Problem1.Models;

namespace Problem1.Services
{
    public class ProducerManager : BaseManager
    {
        private const int MAX_SIZE_OF_QUEUE = 100;

        private readonly IReadWriteData _readWriteData;
        private readonly AutoResetEvent _producerEvent;

        public ProducerManager(IReadWriteData writeData)
        {
            _readWriteData = writeData;
            _producerEvent = new AutoResetEvent(true);

            _canWriteAction = OnCanWriteAction;
        }

        public void ProducerWork(object? additionalData)
        {
            var producerId = additionalData != null ? (int)additionalData : -1;

            while (true)
            {
                Console.WriteLine($"W_Write:{producerId}");
                _producerEvent.WaitOne();
                Console.WriteLine($"E_Write:{producerId}");

                RandomThreadSleep();

                //lock (_readWriteData.LockObject)
                //{
                    var count = _readWriteData.Count();
                    if (count == MAX_SIZE_OF_QUEUE)
                    {
                        Console.WriteLine($"W_Write:{producerId}, Count:{count}");
                        _stopWriteAction.Invoke();
                        continue;
                    }

                    var value = _random.Next(int.MinValue, int.MaxValue);
                    _readWriteData.Write(value);

                    if (count == 0)
                    {
                        _canReadAction.Invoke();
                    }

                    _producerEvent.Set();
                //}
            }
        }

        private void OnCanWriteAction()
        {
            Console.WriteLine("OnCanWriteAction");
            _producerEvent.Set();
        }
    }
}
