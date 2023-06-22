namespace Problem1.Services
{
    public abstract class BaseManager
    {
        protected static Random _random;

        protected static Action _canReadAction;
        protected static Action _canWriteAction;
        protected static Action _stopWriteAction;

        //protected static ManualResetEvent _producerEvent;
        //protected static ManualResetEvent _consumerEvent;

        static BaseManager()
        {
            //_producerEvent = new ManualResetEvent(true);
            //_consumerEvent = new ManualResetEvent(false);

            _random = new Random();
        }

        protected virtual void RandomThreadSleep()
        {
            Thread.Sleep(_random.Next(101));
        }
    }
}
