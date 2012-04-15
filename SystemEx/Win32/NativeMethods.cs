using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SystemEx.Win32
{
    internal static partial class NativeMethods
    {
        /// <summary>
        /// The signature of the callback that receives messages from the Task Dialog when various events occur.
        /// </summary>
        /// <param name="hwnd">The window handle of the </param>
        /// <param name="msg">The message being passed.</param>
        /// <param name="wParam">wParam which is interpreted differently depending on the message.</param>
        /// <param name="lParam">wParam which is interpreted differently depending on the message.</param>
        /// <param name="refData">The refrence data that was set to TaskDialog.CallbackData.</param>
        /// <returns>A HRESULT value. The return value is specific to the message being processed. </returns>
        public delegate int TaskDialogCallback([In] IntPtr hwnd, [In] uint msg, [In] UIntPtr wParam, [In] IntPtr lParam, [In] IntPtr refData);

        public static bool GetShouldShowExtensions()
        {
            SHELLFLAGSTATE state;

            SHGetSettings(out state, SSF_SHOWEXTENSIONS);

            return (state & SHELLFLAGSTATE.fShowExtensions) == SHELLFLAGSTATE.fShowExtensions;
        }

        public static class Util
        {
            internal static int MAKELONG(int low, int high)
            {
                return (high << 16) | (low & 0xffff);
            }

            internal static IntPtr MAKELPARAM(int low, int high)
            {
                return (IntPtr)((high << 16) | (low & 0xffff));
            }

            public static int HIWORD(int n)
            {
                return (n >> 16) & 0xffff;
            }

            public static int HIWORD(IntPtr n)
            {
                return HIWORD(unchecked((int)(long)n));
            }

            public static int LOWORD(int n)
            {
                return n & 0xffff;
            }

            public static int LOWORD(IntPtr n)
            {
                return LOWORD(unchecked((int)(long)n));
            }

            public static int SignedHIWORD(IntPtr n)
            {
                return SignedHIWORD(unchecked((int)(long)n));
            }
            public static int SignedLOWORD(IntPtr n)
            {
                return SignedLOWORD(unchecked((int)(long)n));
            }

            public static int SignedHIWORD(int n)
            {
                int i = (int)(short)((n >> 16) & 0xffff);

                return i;
            }

            public static int SignedLOWORD(int n)
            {
                int i = (int)(short)(n & 0xFFFF);

                return i;
            }
        }
    }
}
