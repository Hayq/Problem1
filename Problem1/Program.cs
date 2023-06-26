using Problem1.Services;

namespace Problem1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var problemManager = new ProblemService();
            problemManager.Start();
            System.Console.ReadLine();
        }
    }
}