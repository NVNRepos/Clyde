using System;

namespace Clyde.Domain
{
    /// <summary>
    /// Interface for StateHolders
    /// </summary>
    public interface IStateHolder : IDisposable
    {
        /// <summary>
        /// Used duration
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// The current <see cref="ExecutionState"/>
        /// </summary>
        ExecutionState State { get; }

        /// <summary>
        /// Start the <see cref="IStateHolder"/>
        /// </summary>
        /// <returns> <see langword="true"/> if successful </returns>
        bool Start();

        /// <summary>
        /// Stop the <see cref="IStateHolder"/>
        /// </summary>
        /// <returns> <see langword="true"/> if successful </returns>
        bool Stop();
    }
}

