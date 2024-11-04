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

        private static void PreventSleep()
            => Win32Facade.SetThreadExecutionState(
                Win32Facade.WinExecutionState.Continuous |
                Win32Facade.WinExecutionState.System_Required |
                Win32Facade.WinExecutionState.Display_Required);

        protected override void AfterStop()
        {
            Win32Facade.SetThreadExecutionState(Win32Facade.WinExecutionState.Continuous);
            base.AfterStop();
        }

        private static void SimulateKeyPress()
        {
            Win32Facade.KeyboardEvent(Win32Facade.VirtualKey.Shift, Win32Facade.KeyEvent.KeyDown);
            Win32Facade.KeyboardEvent(Win32Facade.VirtualKey.Shift, Win32Facade.KeyEvent.KeyUp);
        }

    }
}
