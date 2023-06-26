using Problem1.Models;
using System.Runtime.CompilerServices;

namespace Problem1.Services
{
    public class ConsumerService : BaseService
    {
        private const int MIN_SIZE_OF_QUEUE = 0;
        private const int MIN_LIMIT_OF_WRITE_WAITING = 80;

        private bool _stoppedWriting;

        private readonly IReadData _readData;
        private readonly IFileWrite _fileWrite;
        private readonly IConsumerEvent _consumerEvent;
        private readonly CancellationToken _cancellationToken;
        private readonly AutoResetEvent _consumerSync;

        public ConsumerService(IReadData readData, IFileWrite fileWrite, IConsumerEvent consumerEvent, CancellationToken cancellationToken)
        {
            _readData = readData;
            _fileWrite = fileWrite;
            _cancellationToken = cancellationToken;
            _consumerSync = new AutoResetEvent(false);
            _consumerEvent = consumerEvent;

            _consumerEvent.StartReadAction += OnCanReadAction;
            _consumerEvent.StopWriteAction += OnStopWriteAction;
        }

        public void ConsumerWork()
        {
            var consumerId = Thread.CurrentThread.Name;

            while (!_cancellationToken.IsCancellationRequested || _readData.Count() > 0)
            {
                _consumerSync.WaitOne();

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
                _consumerSync.Set();
                _fileWrite.Append(value);
            }
        }

        private static void SentToTextFile(int value)
        {
            using StreamWriter writer = File.AppendText("output.txt");
            writer.Write($"{value},");
        }

        private void OnCanReadAction()
        {
            _consumerSync.Set();
        }

        private void OnStopWriteAction()
        {
            _stoppedWriting = true;
        }
    }
}
