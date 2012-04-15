using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using SystemEx.Win32;
using Microsoft.Win32;

namespace SystemEx.Windows.Forms
{
    public static class PopupUtil
    {
        private const int IdleTime = 2000;
        private static IDisposable _finalizer;
        private static bool _sessionLocked;

        static PopupUtil()
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            _finalizer = new Finalizer(() =>
            {
                SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            });
        }

        static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    _sessionLocked = true;
                    break;

                case SessionSwitchReason.SessionUnlock:
                    _sessionLocked = false;
                    break;
            }
        }

        public static bool ShouldShowPopup(System.Windows.Forms.Form popupForm, Screen popupScreen)
        {
            // Is the current session locked?

            if (_sessionLocked)
                return false;

            // Has the last mouse movement or keyboard action been too long ago?

            var lii = new NativeMethods.LASTINPUTINFO();

            lii.cbSize = (uint)Marshal.SizeOf(lii);

            bool fResult = NativeMethods.GetLastInputInfo(ref lii);

            if (!fResult)
                throw new Exception("GetLastInputInfo failed");

            if (NativeMethods.GetTickCount() - lii.dwTime > IdleTime)
            {
                return false;
            }

            // Only consider the foreground window when it is on the same monitor
            // as the popup is going to be displayed on.

            var hForeground = NativeMethods.GetForegroundWindow();

            var screen = Screen.FromHandle(hForeground);

            if (screen.WorkingArea != popupScreen.WorkingArea)
            {
                return true;
            }

            // Is the foreground application running in full-screen mode?

            NativeMethods.RECT rcForeground = new NativeMethods.RECT();

            NativeMethods.GetClientRect(hForeground, ref rcForeground);

            var foreground = ClientToScreen(hForeground, rcForeground);

            // If the client rect is covering the entire screen, the application is a
            // full-screen application.

            return !(
                screen.Bounds.Left >= foreground.Left &&
                screen.Bounds.Top >= foreground.Top &&
                screen.Bounds.Right <= foreground.Right &&
                screen.Bounds.Bottom <= foreground.Bottom
            );
        }

        private static Rectangle ClientToScreen(IntPtr handle, Rectangle rect)
        {
            var p1 = new Point(rect.Left, rect.Top);
            var p2 = new Point(rect.Right, rect.Bottom);

            NativeMethods.ClientToScreen(handle, ref p1);
            NativeMethods.ClientToScreen(handle, ref p2);

            return new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
        }
    }
}
