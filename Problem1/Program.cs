using Problem1.Services;

namespace Problem1
{
    public class Program
    {
        const int N = 10;
        const int M = 10;

        static void Main(string[] args)
        {
            var problemManager = new ProblemManager(N, M);
            problemManager.Start();

            Console.ReadKey();// another task for listening inputs and react as needed !!!
        }
    }
}