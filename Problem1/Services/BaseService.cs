namespace Problem1.Services
{
    public abstract class BaseService
    {
        protected static readonly Random _random = new Random();

        protected virtual void RandomThreadSleep()
        {
            Thread.Sleep(_random.Next(101));
        }
    }
}
