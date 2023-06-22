using Problem1.Models;

namespace Problem1.Services
{
    public class DisplayService
    {
        private readonly IReadData _readData;
        private readonly CancellationToken _cancellationToken;

        public DisplayService(IReadData readData, CancellationToken cancellationToken)
        {
            _readData = readData;
            _cancellationToken = cancellationToken;
        }

        public void DisplayDataCount()
        {
            while (!_cancellationToken.IsCancellationRequested || _readData.Count() > 0)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Queue count:{_readData.Count()}, Time:{DateTime.Now}");
            }
        }
    }
}
