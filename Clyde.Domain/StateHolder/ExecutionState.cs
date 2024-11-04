namespace Clyde.Domain
{
    /// <summary>
    /// This enum displays an executions state of a <see cref="IStateHolder"/>
    /// </summary>
    public enum ExecutionState
    {
        /// <summary> Idle state</summary>
        Idle,
        /// <summary> Running state</summary>
        Running,
        /// <summary> Disposed </summary>
        Disposed
    }
}
