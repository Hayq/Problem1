namespace Problem.Application.Utility
{
    /// <summary>
    /// Represents an event interface for producers, providing actions to start reading and writing operations.
    /// </summary>
    public interface IProducerEvent
    {
        /// <summary>
        /// Gets the action to start a read operation.
        /// </summary>
        Action StartReadAction { get; }

        /// <summary>
        /// Gets or sets the function to start a write operation.
        /// </summary>
        Func<bool> StartWriteAction { get; set; }

        /// <summary>
        /// Gets the action to stop a write operation.
        /// </summary>
        Action StopWriteAction { get; }
    }

    /// <summary>
    /// Represents an event interface for consumers, providing actions to start reading and writing operations.
    /// </summary>
    public interface IConsumerEvent
    {
        /// <summary>
        /// Gets or sets the action to start a read operation.
        /// </summary>
        Action StartReadAction { get; set; }

        /// <summary>
        /// Gets the function to start a write operation.
        /// </summary>
        Func<bool> StartWriteAction { get; }

        /// <summary>
        /// Gets or sets the action to stop a write operation.
        /// </summary>
        Action StopWriteAction { get; set; }
    }

    /// <summary>
    /// Represents a class that implements both the IProducerEvent and IConsumerEvent interfaces, providing properties for event actions.
    /// </summary>
    public class ProducerConsumerEvent : IProducerEvent, IConsumerEvent
    {
        /// <inheritdoc/>
        public Action StartReadAction { get; set; }

        /// <inheritdoc/>
        public Func<bool> StartWriteAction { get; set; }

        /// <inheritdoc/>
        public Action StopWriteAction { get; set; }
    }
}