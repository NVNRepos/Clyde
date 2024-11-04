using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Clyde.App.Commands;
using Clyde.App.Domain;
using Clyde.App.Resources;
using Clyde.Domain;

namespace Clyde.App.ViewModels
{
    /// <summary> Viewmodel for the Clyde Windows Application </summary>
    public class ClydeWinViewModel : ClydeViewModelBase
    {
        #region Fields

        //Application specifics are in this derived class.
        //Don't work with the backing fields unless you are in the ctor!
        private Brush _iconColor = null;
        private string _statusText = null;

        #endregion

        #region Properties

        /// <summary> Button icon color </summary>
        public Brush IconColor
        {
            get => _iconColor;
            set { _iconColor = value; OnPropertyChanged(nameof(IconColor)); }
        }

        /// <summary> State display </summary>
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(nameof(StatusText)); }
        }

        #endregion

        #region Commands

        public ICommand ButtonCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }

        #endregion

        #region ctor

        /// <summary>
        /// parameterless constructor for XAML
        /// </summary>
        public ClydeWinViewModel() : base(new BasicWinStateHolder())
            => (ButtonCommand, StartCommand, StopCommand)
            = (new RelayCommand(ToggleState), new RelayCommand(StartHolder), new RelayCommand(StopHolder));

        #endregion

        #region Overrides

        protected override void UpdateUIOnState(ExecutionState state)
        {
            switch (state)
            {
                case ExecutionState.Idle:
                    IconColor = (Brush)Application.Current.FindResource(ClydeResourceKey.Color.BUTTON_ON);
                    StatusText = Language.Idle;
                    break;
                case ExecutionState.Running:
                    IconColor = (Brush)Application.Current.FindResource(ClydeResourceKey.Color.BUTTON_OFF);
                    StatusText = Language.Running;
                    break;
                default: break;
            }
            base.UpdateUIOnState(state);
        }

        #endregion
    }
}
