using System;
using System.Windows;
using System.Windows.Interop;

namespace Clyde.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize | ResizeMode.CanMinimize;
        }

        #region WindowInteractions

        private void OnLoad(object sender, EventArgs e)
            => Win32Facade.SetTitlebarColor(new WindowInteropHelper(this).EnsureHandle(), App.RequestedTheme);

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                sysTray?.Dispose(); //this needs to be here for cleanup
            }
            base.OnClosing(e);
        }

        private void Exit(object sender, EventArgs e)
        {
            Hide();
            Close();
        }

        private void Open(object sender, EventArgs e)
        {
            Show();
            Activate();
        }

        #endregion
    }
}
