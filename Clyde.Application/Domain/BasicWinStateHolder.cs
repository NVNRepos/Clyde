using Clyde.Domain;
using static Clyde.App.Win32Facade;

namespace Clyde.App.Domain
{
    public class BasicWinStateHolder : StateHolderBase
    {
        protected override void OnTimerElapsed(object _)
        {
            // Simulate key event
            KeyboardEvent(VirtualKey.Shift, KeyEvent.KeyDown);
            KeyboardEvent(VirtualKey.Shift, KeyEvent.KeyUp);
        }

    }
}
