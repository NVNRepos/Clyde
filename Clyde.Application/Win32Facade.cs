using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace Clyde.App
{
    /// <summary> Interaction with the Win32API and the registry </summary>
    public static class Win32Facade
    {
        #region Constants

        private const string KERNEL = "kernel32.dll";
        private const string DWMAPI = "dwmapi.dll";
        private const string USER = "user32.dll";

        private const string REG_COLOR_SETTINGS = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string APPS_USES_LIGHT_THEME = "AppsUseLightTheme";
        private const string SYSTEM_USES_LIGHT_THEME = "SystemUsesLightTheme";
        private const uint DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        #endregion

        #region Virtualkeys

        /// <summary>
        /// Virtual keys
        /// </summary>
        public enum VirtualKey : byte
        {
            // Modifier keys

            /// <summary> Shift key </summary>
            Shift = 0x10,
            /// <summary> Control key </summary>
            Control = 0x11,
            /// <summary> Alt key </summary>
            Menu = 0x12,

            // Alphabet keys

            /// <summary> A key </summary>
            A = 0x41,
            /// <summary> B key </summary>
            B = 0x42,
            /// <summary> C key </summary>
            C = 0x43,
            /// <summary> D key </summary>
            D = 0x44,
            /// <summary> E key </summary>
            E = 0x45,
            /// <summary> F key </summary>
            F = 0x46,
            /// <summary> G key </summary>
            G = 0x47,
            /// <summary> H key </summary>
            H = 0x48,
            /// <summary> I key </summary>
            I = 0x49,
            /// <summary> J key </summary>
            J = 0x4A,
            /// <summary> K key </summary>
            K = 0x4B,
            /// <summary> L key </summary>
            L = 0x4C,
            /// <summary> M key </summary>
            M = 0x4D,
            /// <summary> N key </summary>
            N = 0x4E,
            /// <summary> O key </summary>
            O = 0x4F,
            /// <summary> P key </summary>
            P = 0x50,
            /// <summary> Q key </summary>
            Q = 0x51,
            /// <summary> R key </summary>
            R = 0x52,
            /// <summary> S key </summary>
            S = 0x53,
            /// <summary> T key </summary>
            T = 0x54,
            /// <summary> U key </summary>
            U = 0x55,
            /// <summary> V key </summary>
            V = 0x56,
            /// <summary> W key </summary>
            W = 0x57,
            /// <summary> X key </summary>
            X = 0x58,
            /// <summary> Y key </summary>
            Y = 0x59,
            /// <summary> Z key </summary>
            Z = 0x5A,

            // Numeric keys (Top row)

            /// <summary> 0 key </summary>
            Number0 = 0x30,
            /// <summary> 1 key </summary>
            Number1 = 0x31,
            /// <summary> 2 key </summary>
            Number2 = 0x32,
            /// <summary> 3 key </summary>
            Number3 = 0x33,
            /// <summary> 4 key </summary>
            Number4 = 0x34,
            /// <summary> 5 key </summary>
            Number5 = 0x35,
            /// <summary> 6 key </summary>
            Number6 = 0x36,
            /// <summary> 7 key </summary>
            Number7 = 0x37,
            /// <summary> 8 key </summary>
            Number8 = 0x38,
            /// <summary> 9 key </summary>
            Number9 = 0x39,

            // Function keys

            /// <summary> F1 key </summary>
            F1 = 0x70,
            /// <summary> F2 key </summary>
            F2 = 0x71,
            /// <summary> F3 key </summary>
            F3 = 0x72,
            /// <summary> F4 key </summary>
            F4 = 0x73,
            /// <summary> F5 key </summary>
            F5 = 0x74,
            /// <summary> F6 key </summary>
            F6 = 0x75,
            /// <summary> F7 key </summary>
            F7 = 0x76,
            /// <summary> F8 key </summary>
            F8 = 0x77,
            /// <summary> F9 key </summary>
            F9 = 0x78,
            /// <summary> F10 key </summary>
            F10 = 0x79,
            /// <summary> F11 key </summary>
            F11 = 0x7A,
            /// <summary> F12 key </summary>
            F12 = 0x7B,

            // Control keys

            /// <summary> Escape key </summary>
            Escape = 0x1B,
            /// <summary> Enter key </summary>
            Enter = 0x0D,
            /// <summary> Spacebar </summary>
            Space = 0x20,
            /// <summary> Backspace key </summary>
            Backspace = 0x08,
            /// <summary> Tab key </summary>
            Tab = 0x09,
            /// <summary> Caps Lock key </summary>
            CapsLock = 0x14,

            // Navigation keys

            /// <summary> Left arrow key </summary>
            Left = 0x25,
            /// <summary> Up arrow key </summary>
            Up = 0x26,
            /// <summary> Right arrow key </summary>
            Right = 0x27,
            /// <summary> Down arrow key </summary>
            Down = 0x28,
            /// <summary> Home key </summary>
            Home = 0x24,
            /// <summary> End key </summary>
            End = 0x23,
            /// <summary> Page Up key </summary>
            PageUp = 0x21,
            /// <summary> Page Down key </summary>
            PageDown = 0x22,

            // Insert/Delete keys

            /// <summary> Insert key </summary>
            Insert = 0x2D,
            /// <summary> Delete key </summary>
            Delete = 0x2E,

            // Numeric keypad keys

            /// <summary> Numeric keypad 0 key </summary>
            NumPad0 = 0x60,
            /// <summary> Numeric keypad 1 key </summary>
            NumPad1 = 0x61,
            /// <summary> Numeric keypad 2 key </summary>
            NumPad2 = 0x62,
            /// <summary> Numeric keypad 3 key </summary>
            NumPad3 = 0x63,
            /// <summary> Numeric keypad 4 key </summary>
            NumPad4 = 0x64,
            /// <summary> Numeric keypad 5 key </summary>
            NumPad5 = 0x65,
            /// <summary> Numeric keypad 6 key </summary>
            NumPad6 = 0x66,
            /// <summary> Numeric keypad 7 key </summary>
            NumPad7 = 0x67,
            /// <summary> Numeric keypad 8 key </summary>
            NumPad8 = 0x68,
            /// <summary> Numeric keypad 9 key </summary>
            NumPad9 = 0x69,
            /// <summary> Multiply key </summary>
            NumPadMultiply = 0x6A,
            /// <summary> Add key </summary>
            NumPadAdd = 0x6B,
            /// <summary> Subtract key </summary>
            NumPadSubtract = 0x6D,
            /// <summary> Decimal key </summary>
            NumPadDecimal = 0x6E,
            /// <summary> Divide key </summary>
            NumPadDivide = 0x6F,

            // Special keys

            /// <summary> Print Screen key </summary>
            PrintScreen = 0x2C,
            /// <summary> Scroll Lock key </summary>
            ScrollLock = 0x91,
            /// <summary> Pause key </summary>
            Pause = 0x13,

            // Windows keys

            /// <summary> Left Windows key </summary>
            LWin = 0x5B,
            /// <summary> Right Windows key </summary>
            RWin = 0x5C,

            // Other keys

            /// <summary> Applications key (Context Menu key) </summary>
            Apps = 0x5D,
            /// <summary> Computer Sleep key </summary>
            Sleep = 0x5F
        }

        #endregion

        #region WinExecutionState

        /// <summary>
        /// Windows executionsstate
        /// </summary>
        public enum WinExecutionState : uint
        {
            /// <summary> No sleepmode or standby </summary>
            System_Required = 0x00000001,
            /// <summary> Display always on </summary>
            Display_Required = 0x00000002,
            /// <summary> energy saving mode </summary>
            Anymode_Required = 0x00000040,
            /// <summary> State will be maintained </summary>
            Continuous = 0x80000000
        }

        #endregion

        #region KeyEvents

        /// <summary>
        /// Windows key event flags
        /// </summary>
        public enum KeyEvent : uint
        {
            /// <summary> Key press </summary>
            KeyDown = 0x0000,
            /// <summary> Extended key like numpadkeys </summary>
            ExtendedKey = 0x0001,
            /// <summary> Key release </summary>
            KeyUp = 0x0002,
            /// <summary> Identify key </summary>
            ScanCode = 0x0004,
            /// <summary> Simulate Unicode </summary>
            UniCode = 0x0008,
        }

        #endregion

        #region DllImports

        [DllImport(KERNEL, SetLastError = true)]
        private static extern uint SetThreadExecutionState(uint esFlag);

        [DllImport(USER, SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport(DWMAPI, PreserveSig = false)]
        private static extern void DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref uint attrValue, uint attrSize);

        #endregion

        #region Methods

        /// <summary>
        /// Currently used system settings for themes (Light/Dark)
        /// </summary>
        /// <returns></returns>
        public static ApplicationTheme SystemTheme()
        {
            try
            {
                object appsUseLightTheme = Registry.GetValue(REG_COLOR_SETTINGS, APPS_USES_LIGHT_THEME, 1);
                object systemUseLightTheme = Registry.GetValue(REG_COLOR_SETTINGS, SYSTEM_USES_LIGHT_THEME, 1);

                bool isAppInDarkMode = (int)appsUseLightTheme == 0;
                bool isSystemInDarkMode = (int)systemUseLightTheme == 0;
                return (isAppInDarkMode || isSystemInDarkMode) ? ApplicationTheme.Dark : ApplicationTheme.Light;
            }
            catch
            {
                return ApplicationTheme.Light;
            }
        }

        /// <summary>
        /// Sets the non client area (titlebar) color of a handle (handle should be a Window)
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="theme"></param>
        public static void SetTitlebarColor(IntPtr handle, ApplicationTheme theme)
        {
            uint usedtheme = (uint)theme;
            DwmSetWindowAttribute(handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref usedtheme, sizeof(uint));
        }

        /// <summary>
        /// Triggers a Keyevent
        /// </summary>
        /// <param name="key">Key that should trigger the event</param>
        /// <param name="event">Event that should be used</param>
        public static void KeyboardEvent(VirtualKey key, KeyEvent @event)
            => keybd_event((byte)key, 0, (uint)@event, UIntPtr.Zero);

        /// <summary>
        /// Sets the Windows Exceution state
        /// </summary>
        /// <param name="state">State that should be used</param>
        /// <returns>The current State (if changed it should be the same a requested)</returns>
        public static WinExecutionState SetThreadExecutionState(WinExecutionState state)
            => (WinExecutionState)SetThreadExecutionState((uint)state);

        #endregion
    }
}
