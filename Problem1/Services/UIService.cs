namespace Problem1.Services
{
    public class UIService
    {
        private Action<int> EnteredProducersCountEvent;
        private Action<int> EnteredConsumersCountEvent;
        private Action StartApp;
        private Action StopApp;

        public UIService(IProblemService service)
        {
            EnteredProducersCountEvent = service.InitProducer;
            EnteredConsumersCountEvent = service.InitConsumer;
            StartApp = service.StartThread;
            StopApp = service.StopThread;
        }

        public void Start()
        {
            SetProducersCount();
            SetConsumersCount();
            StartApp.Invoke();
            WaitQuitCommand();
        }

        private void SetProducersCount()
        {
            var isValidInput = false;

            while(!isValidInput)
            {
                System.Console.Write("Set producer count:");

                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out int number);
                if(!isValidInput)
                {
                    System.Console.WriteLine("Invalid input, please try again");
                    continue;
                }

                isValidInput = number > 0 && number < 11;
                if(!isValidInput)
                {
                    System.Console.WriteLine("Please input integer number in range of 1 to 10");
                    continue;
                }

                EnteredProducersCountEvent(number);
            }
        }
    
        private void SetConsumersCount()
        {
            var isValidInput = false;

            while(!isValidInput)
            {
                System.Console.Write("Set Consumer count:");

                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out int number);
                if(!isValidInput)
                {
                    System.Console.WriteLine("Invalid input, please try again");
                    continue;
                }

                isValidInput = number > 0 && number < 11;
                if(!isValidInput)
                {
                    System.Console.WriteLine("Please input integer number in range of 1 to 10");
                    continue;
                }

                EnteredConsumersCountEvent(number);
            }
        }


        private void WaitQuitCommand()
        {
            System.Console.WriteLine("Press enter to stop application...");

            while(ConsoleKey.Enter != Console.ReadKey().Key);

            StopApp.Invoke();
        }
    }
}