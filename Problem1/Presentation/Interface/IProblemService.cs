namespace Problem.Presentation.Interface
{
    public interface IProblemService
    {
        void InitProducer(int count);
        void InitConsumer(int count);
        void StartThread();
        void StopThread();
    }
}
