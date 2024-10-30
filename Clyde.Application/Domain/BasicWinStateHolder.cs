using Clyde.Domain;

namespace Clyde.App.Domain
{
    public class BasicWinStateHolder : StateHolderBase
    {

        public BasicWinStateHolder() : base() { }

        protected override void OnTimerElapsed(object state)
        {
            PreventSleep();
            SimulateKeyPress();
        }

        private static void SetWinState(Win32Facade.WinExecutionState winState)
            => Win32Facade.SetThreadExecutionState(winState);

        private static void PreventSleep()
            => SetWinState(
                Win32Facade.WinExecutionState.Continuous |
                Win32Facade.WinExecutionState.System_Required |
                Win32Facade.WinExecutionState.Display_Required);

        protected override void AfterStop()
            => SetWinState(Win32Facade.WinExecutionState.Continuous);

        private static void SimulateKeyPress()
        {
            Win32Facade.KeyboardEvent(Win32Facade.VirtualKey.Shift, Win32Facade.KeyEvent.KeyDown);
            Win32Facade.KeyboardEvent(Win32Facade.VirtualKey.Shift, Win32Facade.KeyEvent.KeyUp);
        }

    }
}
