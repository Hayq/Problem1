using Problem.Infrastructure.Domain;

namespace Problem.Application
{
    public class DisplayService : BaseService
    {
        private readonly IReadData _readData;

        public DisplayService(IReadData readData, CancellationToken cancellationToken)
            : base(cancellationToken)
        {
            _readData = readData;
        }

        protected override void RandomThreadSleep()
        {
            Thread.Sleep(1000);
        }

        public override void Work()
        {
            while (!_cancellationToken.IsCancellationRequested || _readData.Count() > 0)
            {
                RandomThreadSleep();
                Console.WriteLine($"Queue count:{_readData.Count()}");
            }
        }
    }
}
