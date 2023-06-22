using Problem1.Models;

namespace Problem1.Services
{
    public class ProblemService
    {
        private SharedData _data = new SharedData();
        private List<Thread> _producers;
        private List<Thread> _consumers;
        private Thread _display;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ProblemService(int n, int m)
        {
            _producers = new List<Thread>(n);
            _consumers = new List<Thread>(m);

            //initiation
            var eventService = new ProducerConsumerEventService();
            var producerManager = new ProducerService(_data, eventService, _cancellationTokenSource.Token);
            var consumerManager = new ConsumerService(_data, eventService, _cancellationTokenSource.Token);
            var displayManager = new DisplayService(_data, _cancellationTokenSource.Token);

            _display = new Thread(displayManager.DisplayDataCount)
            {
                IsBackground = true
            };

            for (int i = 0; i < n; i++)
            {
                var thread = new Thread(producerManager.ProducerWork)
                {
                    Name = $"thread_p_{i}",
                    IsBackground = true
                };

                _producers.Add(thread);
            }

            //consumers initiation
            for (int i = 0; i < m; i++)
            {
                var thread = new Thread(consumerManager.ConsumerWork)
                {
                    Name = $"thread_c_{i}",
                    IsBackground = true
                };

                _consumers.Add(thread);
            }
        }

        public void Start()
        {
            Parallel.ForEach(_producers, p => p.Start());
            Parallel.ForEach(_consumers, c => c.Start());
            _display.Start();

            //move to UI service manager
            Console.ReadLine();
            _cancellationTokenSource.Cancel();
        }
    }
}
