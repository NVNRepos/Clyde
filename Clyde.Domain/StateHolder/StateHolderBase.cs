using System.Threading;
using System;

namespace Clyde.Domain
{
    /// <summary>
    /// Base class for timer based <see cref="IStateHolder"/> implementations.
    /// </summary>
    public abstract class StateHolderBase : IStateHolder
    {
        #region Fields

        private readonly Timer _timer = null;
        private readonly System.Diagnostics.Stopwatch _watch;

        #endregion

        #region Properties

        public ExecutionState State { get; private set; }

        public TimeSpan Duration => _watch.Elapsed;

        /// <summary>
        /// Period for the internal timer
        /// <para>When override do not use negative integers or zero</para>
        /// </summary>
        protected virtual int Period => 60000; // One minute

        #endregion

        #region ctor

        protected StateHolderBase()
        {
            State = ExecutionState.Idle;
            _timer = new Timer(OnTimerElapsed, this, Timeout.Infinite, Period);
            _watch = new System.Diagnostics.Stopwatch();
        }

        #endregion

        #region IStateHolder

        public bool Start()
        {
            if (State != ExecutionState.Idle)
                return false;

            BeforeStart();

            _timer.Change(0, Period);
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

            _timer.Change(Timeout.Infinite, Period);
            _watch.Stop();
            State = ExecutionState.Idle;

            AfterStop();

            return true;
        }

        #endregion

        #region TimerEvent

        /// <summary>
        /// Method will be called when <see cref="Period"/> is elapsed
        /// </summary>
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
            _timer?.Dispose();
            State = ExecutionState.Disposed;
        }

        #endregion
    }
}

