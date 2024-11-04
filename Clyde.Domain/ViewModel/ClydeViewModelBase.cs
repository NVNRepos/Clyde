using System;
using System.ComponentModel;
using System.Threading;

namespace Clyde.Domain
{
    /// <summary>
    /// Base class for Clyde business logic
    /// </summary>
    public class ClydeViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private const int REFRESH_RATE = 500; // 0.5 seconds
        private const string TIME_FORMAT = @"hh\:mm\:ss";
        private readonly IStateHolder _stateHolder;
        private readonly Timer _timer;

        // Don't work with the backing fields unless you are in the ctor!
        private string _statusTime;
        private bool _canStart = false;
        private bool _canStop = false;

        #endregion

        #region Properties

        /// <summary> Usage duration </summary>
        public string StatusTime
        {
            get => _statusTime;
            private set { _statusTime = value; OnPropertyChanged(nameof(StatusTime)); }
        }

        /// <summary> If <see cref="StartHolder"/> can be performed with effect </summary>
        public bool CanStart
        {
            get => _canStart;
            private set { _canStart = value; OnPropertyChanged(nameof(CanStart)); }
        }

        /// <summary> If <see cref="StopHolder"/> can performed with effect </summary>
        public bool CanStop
        {
            get => _canStop;
            private set { _canStop = value; OnPropertyChanged(nameof(CanStop)); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stateHolder"> The implementation </param>
        public ClydeViewModelBase(IStateHolder stateHolder)
        {
            _stateHolder = stateHolder;

            //timer
            _timer = new Timer(UpdateStatusTime, this, Timeout.Infinite, REFRESH_RATE); 
            AdjustTimer();

            _statusTime = _stateHolder.Duration.ToString(TIME_FORMAT);
            SetProperties();
        }

        #endregion

        #region Methods

        private void AdjustTimer()
        {
            if (_stateHolder.State == ExecutionState.Running)
                _timer.Change(0, REFRESH_RATE);
        }

        private void UpdateStatusTime(object state)
            => StatusTime = _stateHolder.Duration.ToString(TIME_FORMAT);

        /// <summary> Toggle for buttons </summary>
        public void ToggleState()
        {
            switch (_stateHolder.State)
            {
                case ExecutionState.Idle: StartHolder(); break;
                case ExecutionState.Running: StopHolder(); break;
                default: break;
            }
        }

        /// <summary> Start </summary>
        public void StartHolder()
        {
            if (_stateHolder.Start())
            {
                _timer.Change(0, REFRESH_RATE);
                SetProperties();
            }
        }

        /// <summary> Stop </summary>
        public void StopHolder()
        {
            if (_stateHolder.Stop())
            {
                _timer.Change(Timeout.Infinite, REFRESH_RATE);
                SetProperties();
            }
        }

        private void SetProperties()
        {
            CanStart = _stateHolder.State == ExecutionState.Idle;
            CanStop = _stateHolder.State == ExecutionState.Running;
            UpdateUIOnState(_stateHolder.State);
        }

        /// <summary> Update the UI depending on the state </summary>
        /// <param name="state">The current <see cref="ExecutionState"/></param>
        protected virtual void UpdateUIOnState(ExecutionState state) { }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Invoke the <see cref="PropertyChanged"/> event </summary>
        /// <param name="propertyName"> Name of the property that changed </param>
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region IDisposable

        public virtual void Dispose()
        {
            _stateHolder?.Dispose();
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            SetProperties();
        }

        #endregion
    }
}
