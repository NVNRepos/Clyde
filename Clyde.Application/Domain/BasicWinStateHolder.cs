using Clyde.Domain;
using static Clyde.App.Win32Facade;

namespace Clyde.App.Domain
{
    public class BasicWinStateHolder : StateHolderBase
    {
        protected override void OnTimerElapsed(object state)
        {
            PreventSleep();
            SimulateKeyPress();
        }

        private static void PreventSleep()
            => SetThreadExecutionState(
                WinExecutionState.Continuous |
                WinExecutionState.System_Required |
                WinExecutionState.Display_Required);

        protected override void AfterStop()
        {
            SetThreadExecutionState(WinExecutionState.Continuous);
            base.AfterStop();
        }

        private static void SimulateKeyPress()
        {
            KeyboardEvent(VirtualKey.Shift, KeyEvent.KeyDown);
            KeyboardEvent(VirtualKey.Shift, KeyEvent.KeyUp);
        }

    }
}
