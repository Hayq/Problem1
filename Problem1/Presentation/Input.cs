using Problem.Presentation.Interface;

namespace Problem.Presentation
{
    public class Input
    {
        private readonly Action<int> EnteredProducersCountEvent;
        private readonly Action<int> EnteredConsumersCountEvent;
        private readonly Action StartApp;
        private readonly Action StopApp;

        public Input(IProblemService service)
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

            while (!isValidInput)
            {
                Console.Write("Set producer count:");

                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out int number);
                if (!isValidInput)
                {
                    Console.WriteLine("Invalid input, please try again");
                    continue;
                }

                isValidInput = number > 0 && number < 11;
                if (!isValidInput)
                {
                    Console.WriteLine("Please input integer number in range of 1 to 10");
                    continue;
                }

                EnteredProducersCountEvent(number);
            }
        }

        private void SetConsumersCount()
        {
            var isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Set consumer count:");

                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out int number);
                if (!isValidInput)
                {
                    Console.WriteLine("Invalid input, please try again");
                    continue;
                }

                isValidInput = number > 0 && number < 11;
                if (!isValidInput)
                {
                    Console.WriteLine("Please input integer number in range of 1 to 10");
                    continue;
                }

                EnteredConsumersCountEvent(number);
            }
        }

        private void WaitQuitCommand()
        {
            Console.WriteLine("Press enter to stop the application...");

            while (ConsoleKey.Enter != Console.ReadKey().Key)
            {
            }

            StopApp.Invoke();
        }
    }
}