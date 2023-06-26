using System.Collections.Concurrent;

namespace Problem1.Models
{
    public interface IReadData
    {
        int Get();
        int Count();
    }

    public interface IReadWriteData : IReadData
    {
        void Write(int value);
    }

    public class SharedData : IReadWriteData
    {
        private readonly ConcurrentQueue<int> _data;

        public SharedData()
        {
            _data = new ConcurrentQueue<int>();
        }

        public int Get()
        {
            var isSucceeded = _data.TryDequeue(out int result);

            if (!isSucceeded)
            {
                Console.WriteLine($"Can't dequeue, Value:{result}");
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
