namespace Problem1.Services
{
    public interface IProducerEvent
    {
        Action? StartReadAction { get; }
        Func<bool>? StartWriteAction { get; set; }
        Action? StopWriteAction { get; }
    }

    public interface IConsumerEvent
    {
        Action? StartReadAction { get; set; }
        Func<bool>? StartWriteAction { get; }
        Action? StopWriteAction { get; set; }
    }

    public class ProducerConsumerEventService : IProducerEvent, IConsumerEvent
    {
        public Action? StartReadAction { get; set; }

        public Func<bool>? StartWriteAction { get; set; }

        public Action? StopWriteAction { get; set; }
    }
}