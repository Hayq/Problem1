using Problem1.Models;
using System.Collections.Concurrent;

namespace Problem1.Services
{
    public class ProblemManager
    {
        private SharedData _data = new SharedData();
        private List<Thread> _producers;
        private List<Thread> _consumers;

        public ProblemManager(int n, int m)
        {
            _producers = new List<Thread>(n);
            _consumers = new List<Thread>(m);

            //producers initiation
            var producerManager = new ProducerManager(_data);
            var consumerManager = new ConsumerManager(_data);

            for (int i = 0; i < n; i++)
            {
                var thread = new Thread(producerManager.ProducerWork)
                {
                    IsBackground = true
                };

                _producers.Add(thread);
            }

            //consumers initiation
            for (int i = 0; i < m; i++)
            {
                var thread = new Thread(consumerManager.ConsumerWork)
                {
                    IsBackground = true
                };

                _consumers.Add(thread);
            }
        }

        public void Start()
        {
            for (int i = 0; i < _producers.Count; i++)
            {
                _producers[i].Start(i);
            }

            for (int i = 0; i < _consumers.Count; i++)
            {
                _consumers[i].Start(i);
            }
        }
    }
}
