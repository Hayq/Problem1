using Problem1.Services;

namespace Problem1
{
    public class Program
    {
        const int N = 5;
        const int M = 1;

        static void Main(string[] args)
        {


            var problemManager = new ProblemService(N, M);
            problemManager.Start();

            Console.ReadKey();// another task for listening inputs and react as needed !!!
        }
    }
}