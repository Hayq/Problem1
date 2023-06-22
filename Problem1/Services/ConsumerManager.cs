using Problem1.Models;
using System.Runtime.CompilerServices;

namespace Problem1.Services
{
    public class ConsumerManager : BaseManager
    {
        private const int MIN_SIZE_OF_QUEUE = 0;
        private const int MIN_LIMIT_OF_WRITE_WAITING = 80;

        private bool _stoppedWriting;

        private readonly IReadData _readData;
        private readonly AutoResetEvent _consumerEvent;

        public ConsumerManager(IReadData readData)
        {
            _readData = readData;
            _consumerEvent = new AutoResetEvent(false);

            _canReadAction = OnCanReadAction;
            _stopWriteAction = OnStopWriteAction;
        }

        public void ConsumerWork(object? additionalData)
        {
            var consumerId = additionalData != null ? (int)additionalData : -1;

            while (true)
            {
                Console.WriteLine($"W_Read:{consumerId}");
                _consumerEvent.WaitOne();
                Console.WriteLine($"E_Read:{consumerId}");

                RandomThreadSleep();

                //lock (_readData.LockObject)
                //{
                var count = _readData.Count();
                if (count == MIN_SIZE_OF_QUEUE)
                {
                    Console.WriteLine($"W_Read:{consumerId}, Count:{count}");
                    continue;
                }

                if (_stoppedWriting && count < MIN_LIMIT_OF_WRITE_WAITING)
                {
                    _canWriteAction.Invoke();
                    _stoppedWriting = false;
                }

                var value = _readData.Get();
                _consumerEvent.Set();
                Console.WriteLine($"Read element:{value}, Count:{count}");
                SentToTextFile(value);
                //}
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void SentToTextFile(int value)
        {
            using StreamWriter writer = File.AppendText("output.txt");
            writer.Write($"{value},");
        }

        private void OnStopWriteAction()
        {
            _stoppedWriting = true;
        }

        private void OnCanReadAction()
        {
            Console.WriteLine("OnCanReadAction");
            _consumerEvent.Set();
        }
    }
}
