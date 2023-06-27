using Problem.Application;
using Problem.Application.Utility;
using Problem.Infrastructure;
using Problem.Presentation.Interface;

namespace Problem.Presentation
{
    public class Problem : IProblemService
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly FileDataContext _fileService;
        private readonly QueueDataContext _queueData;

        private readonly Input _uiService;
        private readonly ProducerConsumerEvent _eventService;

        private List<Thread> _producers;
        private List<Thread> _consumers;

        private readonly Thread _display;

        public Problem()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _fileService = new FileDataContext();
            _queueData = new QueueDataContext();

            _uiService = new Input(this);
            _eventService = new ProducerConsumerEvent();

            var displayService = new DisplayService(_queueData, _cancellationTokenSource.Token);
            _display = new Thread(displayService.Work);
        }

        public void InitProducer(int count)
        {
            var producerManager = new ProducerService(_queueData, _eventService, _cancellationTokenSource.Token);
            _producers = new List<Thread>(count);

            for (int i = 0; i < count; i++)
            {
                var thread = new Thread(producerManager.Work);
                _producers.Add(thread);
            }
        }

        public void InitConsumer(int count)
        {
            var consumerManager = new ConsumerService(_queueData, _fileService, _eventService, _cancellationTokenSource.Token);
            _consumers = new List<Thread>(count);

            for (int i = 0; i < count; i++)
            {
                var thread = new Thread(consumerManager.Work);
                _consumers.Add(thread);
            }
        }

        public void Start()
        {
            _uiService.Start();
        }

        public void StartThread()
        {
            Parallel.ForEach(_producers, p => p.Start());
            Parallel.ForEach(_consumers, c => c.Start());
            _display.Start();
        }

        public void StopThread()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
