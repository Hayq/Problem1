using Problem.Infrastructure.Domain;
using System.Collections.Concurrent;

namespace Problem.Infrastructure
{
    public class QueueDataContext : IReadWriteData
    {
        private readonly ConcurrentQueue<int> _data;

        public QueueDataContext()
        {
            _data = new ConcurrentQueue<int>();
        }

        public int Get()
        {
            _data.TryDequeue(out int result);
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
