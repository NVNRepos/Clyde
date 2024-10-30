using Clyde.App.Commands;
using Clyde.Domain;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Clyde.App.ViewModels
{
    public class ClydeMainViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private readonly IStateHolder _stateHolder;
        private readonly DispatcherTimer _timer;

        private string _statusTime;
        private bool _canStart = false;
        private bool _canStop = false;

        #endregion

        #region Properties

        public string StatusTime
        {
            get => _statusTime;
            set { _statusTime = value; OnPropertyChanged(nameof(StatusTime)); }
        }

        public bool CanStart
        {
            get => _canStart;
            set { _canStart = value; OnPropertyChanged(nameof(CanStart)); }
        }

        public bool CanStop
        {
            get => _canStop;
            set { _canStop = value; OnPropertyChanged(nameof(CanStop)); }
        }

        #endregion

        #region Command

        public ICommand ButtonCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }

        #endregion

        #region ctor

        public ClydeMainViewModel(IStateHolder stateHolder)
        {
            _stateHolder = stateHolder;

            //timer
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += UpdateStatusTime;

            //commands
            ButtonCommand = new RelayCommand(ToggleState);
            StartCommand = new RelayCommand(StartHolder);
            StopCommand = new RelayCommand(StopHolder);

            _statusTime = _stateHolder.GetFormattedDuration();
            UpdateUIOnState();
        }

        #endregion

        #region Methods

        private void UpdateStatusTime(object state, EventArgs e)
            => StatusTime = _stateHolder.GetFormattedDuration();

        private void ToggleState()
        {
            switch (_stateHolder.State)
            {
                case ExecutionState.Idle: StartHolder(); break;
                case ExecutionState.Running: StopHolder(); break;
                default: break;
            }
        }

        private void StartHolder()
        {
            if (_stateHolder.Start())
            {
                _timer.Start();
                UpdateUIOnState();
            }
        }

        private void StopHolder()
        {
            if (_stateHolder.Stop())
            {
                _timer.Stop();
                UpdateUIOnState();
            }
        }

        private void UpdateUIOnState()
        {
            CanStart = _stateHolder.CanStart();
            CanStop = _stateHolder.CanStop();
            switch (_stateHolder.State)
            {
                case ExecutionState.Idle: UpdateUIOnStart(); break;
                case ExecutionState.Running: UpdateUIOnStop(); break;
                default: break;
            }
        }

        protected virtual void UpdateUIOnStart() { }

        protected virtual void UpdateUIOnStop() { }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region IDisposable

        public virtual void Dispose()
        {
            _timer?.Stop();
            if (_timer != null)
                _timer.Tick -= UpdateStatusTime;
            _stateHolder?.Dispose();
        }

        #endregion
    }
}
