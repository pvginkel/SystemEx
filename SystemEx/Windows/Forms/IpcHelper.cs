using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SystemEx.Win32;

namespace SystemEx.Windows.Forms
{
    public class IpcHelper : IDisposable
    {
        private bool _disposed;

        private Mutex _mutex;

        public IpcHelper(string mutexName)
        {
            if (mutexName == null)
                throw new ArgumentNullException("mutexName");

            bool createdNew;

            _mutex = new Mutex(true, mutexName, out createdNew);

            IsOwned = createdNew;
        }

        public bool IsOwned { get; private set; }

        public static void SendData(string windowName, byte[] data)
        {
            // Find the main window of the other instance.

            var handle = IntPtr.Zero;

            for (int i = 0; i < 3; i++)
            {
                handle = NativeMethods.FindWindow(null, windowName);

                if (handle != IntPtr.Zero)
                    break;

                // Application may still be starting.

                Thread.Sleep(320);
            }

            if (handle == IntPtr.Zero)
                return;

            // Allow set foreground window.

            uint processId;

            NativeMethods.GetWindowThreadProcessId(handle, out processId);

            NativeMethods.AllowSetForegroundWindow(processId);

            // Send the arguments.

            var payload = NativeMethods.LocalAlloc(0x40, (UIntPtr)data.Length);

            try
            {
                NativeMethods.COPYDATASTRUCT copyData = new NativeMethods.COPYDATASTRUCT();

                copyData.dwData = (IntPtr)1;
                copyData.lpData = payload;
                copyData.cbData = data.Length;

                Marshal.Copy(data, 0, copyData.lpData, data.Length);

                NativeMethods.SendMessage(handle, NativeMethods.WM_COPYDATA, IntPtr.Zero, ref copyData);
            }
            finally
            {
                NativeMethods.LocalFree(payload);
            }
        }

        public static byte[] ReceiveData(ref Message m)
        {
            if (m.Msg != NativeMethods.WM_COPYDATA)
                throw new InvalidOperationException("Message is not a WM_COPYDATA message");

            var copyData = (NativeMethods.COPYDATASTRUCT)Marshal.PtrToStructure(
                m.LParam, typeof(NativeMethods.COPYDATASTRUCT)
            );

            var data = new byte[copyData.cbData];

            Marshal.Copy(copyData.lpData, data, 0, copyData.cbData);

            return data;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_mutex != null)
                {
                    ((IDisposable)_mutex).Dispose();
                    _mutex = null;
                }

                _disposed = true;
            }
        }
    }
}
