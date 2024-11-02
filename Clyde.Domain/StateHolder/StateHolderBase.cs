using System.Threading;
using System;

namespace Clyde.Domain
{
    /// <summary>
    /// Base class for <see cref="IStateHolder"/> implementations.
    /// </summary>
    public abstract class StateHolderBase : IStateHolder
    {
        #region Constants

        /// <summary>
        /// Period for the internal timer
        /// </summary>
        public const int PERIOD = 60000; // One minute

        #endregion

        #region Fields

        private Timer _timer = null;
        private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();

        #endregion

        #region Properties

        public ExecutionState State { get; private set; }

        public TimeSpan Duration => _watch.Elapsed;

        #endregion

        #region ctor

        protected StateHolderBase()
            => State = ExecutionState.Idle;

        #endregion

        #region IStateHolder

        public bool Start()
        {
            if (State != ExecutionState.Idle)
                return false;

            BeforeStart();
            _timer = new Timer(OnTimerElapsed, this, 0, PERIOD);
            _watch.Start();
            State = ExecutionState.Running;

            AfterStart();

            return true;
        }

        public bool Stop()
        {
            if (State != ExecutionState.Running)
                return false;

            BeforeStop();

            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            _timer = null;
            _watch.Stop();
            State = ExecutionState.Idle;

            AfterStop();

            return true;
        }

        #endregion

        #region TimerEvent

        /// <summary>
        /// Method will be called when <see cref="PERIOD"/> is elapsed
        /// </summary>
        /// <param name="state"></param>
        protected abstract void OnTimerElapsed(object state);

        /// <summary> Action before <see cref="Start"/> </summary>
        protected virtual void BeforeStart() { }

        /// <summary> Action after <see cref="Start"/> </summary>
        protected virtual void AfterStart() { }

        /// <summary> Action before <see cref="Stop"/> </summary>
        protected virtual void BeforeStop() { }

        /// <summary> Action after <see cref="Stop"/> </summary>
        protected virtual void AfterStop() { }

        #endregion

        #region IDisposable

        public virtual void Dispose()
        {
            Stop();
            _watch?.Reset();
            _watch = null;
            State = ExecutionState.Disposed;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

