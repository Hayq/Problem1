using System.Collections.Concurrent;

namespace Problem1.Models
{
    public interface IReadData
    {
        int Get();
        int Count();
        object LockObject { get; }
    }

    public interface IReadWriteData : IReadData
    {
        void Write(int value);
    }

    public class SharedData : IReadWriteData
    {
        private static readonly object _lockObject = new object();
        private readonly Queue<int> _data;

        public object LockObject { get; } = _lockObject;

        public SharedData()
        {
            _data = new Queue<int>();
        }

        public int Get()
        {
            var isSucceeded = _data.TryDequeue(out int result);

            if (!isSucceeded)
            {
                Console.WriteLine("Can't dequeue");
                //throw new InvalidOperationException("Can't dequeue");
            }

            return result;
        }

        public int Count()
        {
            return _data.Count;
        }

        public void Write(int value)
        {
            _data.Enqueue(value);
        }
    }
}
