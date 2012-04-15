using System.Text;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using SystemEx.Win32;
using Microsoft.Win32;

namespace SystemEx.Windows.Forms
{
    public static class UacUtil
    {
        private const string UacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string UacRegistryValue = "EnableLUA";

        public static bool IsUacEnabled
        {
            get
            {
                RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(UacRegistryKey, false);
                bool result = uacKey.GetValue(UacRegistryValue).Equals(1);
                return result;
            }
        }

        public static bool IsProcessElevated
        {
            get
            {
                if (IsUacEnabled)
                {
                    IntPtr tokenHandle;
                    if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle, NativeMethods.TOKEN_READ, out tokenHandle))
                    {
                        throw new ApplicationException("Could not get process token.  Win32 Error Code: " + Marshal.GetLastWin32Error());
                    }

                    NativeMethods.TOKEN_ELEVATION_TYPE elevationResult = NativeMethods.TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;

                    int elevationResultSize = Marshal.SizeOf((int)elevationResult);
                    uint returnedSize = 0;
                    IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);

                    bool success = NativeMethods.GetTokenInformation(tokenHandle, NativeMethods.TOKEN_INFORMATION_CLASS.TokenElevationType, elevationTypePtr, (uint)elevationResultSize, out returnedSize);
                    if (success)
                    {
                        elevationResult = (NativeMethods.TOKEN_ELEVATION_TYPE)Marshal.ReadInt32(elevationTypePtr);
                        bool isProcessAdmin = elevationResult == NativeMethods.TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                        return isProcessAdmin;
                    }
                    else
                    {
                        throw new ApplicationException("Unable to determine the current elevation.");
                    }
                }
                else
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    bool result = principal.IsInRole(WindowsBuiltInRole.Administrator);
                    return result;
                }
            }
        }

        private const int BCM_FIRST = 0x1600;
        private const int BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        public static void ShowShieldIcon(Button button)
        {
            button.FlatStyle = FlatStyle.System;

            Win32.NativeMethods.SendMessage(button.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }
    }
}
