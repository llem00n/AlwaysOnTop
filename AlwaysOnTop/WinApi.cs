using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Windows
{
    public class WinApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hwnd, int hwndInsertAfter, int x, int y, int w, int h, int flags);

        public static int HWND_TOPMOST = -1, HWND_NOTOPMOST = -2;
        public static int SWP_NOSIZE = 0x0001, SWP_NOMOVE = 0x0002;
    }
}
