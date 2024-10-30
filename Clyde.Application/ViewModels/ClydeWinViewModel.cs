using Clyde.App.Domain;
using Clyde.App.Resources;
using System.Windows;
using System.Windows.Media;

namespace Clyde.App.ViewModels
{
    public class ClydeWinViewModel : ClydeMainViewModel
    {
        #region Fields

        //application specifics are in this derived class.
        private Brush _iconColor = null;
        private string _statusText = null;

        #endregion

        #region Properties

        public Brush IconColor
        {
            get => _iconColor;
            set { _iconColor = value; OnPropertyChanged(nameof(IconColor)); }
        }

        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(nameof(StatusText)); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// parameterless constructor for XAML
        /// </summary>
        public ClydeWinViewModel() : base(new BasicWinStateHolder()) { }

        #endregion

        #region Overrides

        protected override void UpdateUIOnStart()
        {
            IconColor = (Brush)Application.Current.FindResource(ClydeResourceKey.Color.BUTTON_ON);
            StatusText = Language.Idle;
            base.UpdateUIOnStart();
        }

        protected override void UpdateUIOnStop()
        {
            IconColor = (Brush)Application.Current.FindResource(ClydeResourceKey.Color.BUTTON_OFF);
            StatusText = Language.Running;
            base.UpdateUIOnStop();
        }

        #endregion
    }
}
