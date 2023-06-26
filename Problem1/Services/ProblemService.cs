using Problem1.Models;

namespace Problem1.Services
{
    public interface IProblemService
    {
        void InitProducer(int count);
        void InitConsumer(int count);
        void StartThread();
        void StopThread();
    }

    public class ProblemService : IProblemService
    {
        private UIService _uiService;
        private FileService _fileService;
        private ProducerConsumerEvent _eventService;
        private SharedData _data = new SharedData();
        private List<Thread> _producers;
        private List<Thread> _consumers;
        private Thread _display;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ProblemService()
        {
            _uiService = new UIService(this);
            _fileService = new FileService();
            _eventService = new ProducerConsumerEvent();
            
            var displayManager = new DisplayService(_data, _cancellationTokenSource.Token);//this into ui service maybe
            _display = new Thread(displayManager.DisplayDataCount);
        }

        public void InitProducer(int count)
        {
            var producerManager = new ProducerService(_data, _eventService, _cancellationTokenSource.Token);
            _producers = new List<Thread>(count);

            for (int i = 0; i < count; i++)
            {
                var thread = new Thread(producerManager.ProducerWork);
                _producers.Add(thread);
            }
        }

        public void InitConsumer(int count)
        {
            var consumerManager = new ConsumerService(_data, _fileService, _eventService, _cancellationTokenSource.Token);
            _consumers = new List<Thread>(count);

            for (int i = 0; i < count; i++)
            {
                var thread = new Thread(consumerManager.ConsumerWork);
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
