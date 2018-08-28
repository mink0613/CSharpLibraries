using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CommonLibrary
{
    public class Win32API
    {
        #region Structs
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;

            public UInt32 cbData;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        #endregion

        #region Win32API
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        #endregion

        #region Constants
        // Mouse actions: left down
        private const int MOUSE_LEFTDOWN = 0x02;

        // Mouse actions: left up
        private const int MOUSE_LEFTUP = 0x04;

        // Mouse actions: right down
        private const int MOUSE_RIGHTDOWN = 0x08;

        // Mouse actions: right up
        private const int MOUSE_RIGHTUP = 0x10;

        private const int WM_COPYDATA = 0x004A;
        #endregion

        #region Methods
        public static void MouseLeftClick(uint posX = uint.MaxValue, uint posY = uint.MaxValue)
        {
            if (posX == uint.MaxValue)
            {
                posX = (uint)Cursor.Position.X;
            }

            if (posY == uint.MaxValue)
            {
                posY = (uint)Cursor.Position.Y;
            }

            mouse_event(MOUSE_LEFTDOWN | MOUSE_LEFTUP, posX, posY, 0, 0);
        }

        public static void MouseRightClick(uint posX = uint.MaxValue, uint posY = uint.MaxValue)
        {
            if (posX == uint.MaxValue)
            {
                posX = (uint)Cursor.Position.X;
            }

            if (posY == uint.MaxValue)
            {
                posY = (uint)Cursor.Position.Y;
            }

            mouse_event(MOUSE_RIGHTDOWN | MOUSE_RIGHTUP, posX, posY, 0, 0);
        }

        public static bool SendMessage(string processName, string message)
        {
            IntPtr handle = FindWindow(null, processName);
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            COPYDATASTRUCT data = new COPYDATASTRUCT();
            data.dwData = new IntPtr(1024 + 604);
            data.cbData = (uint)message.Length * sizeof(char);
            data.lpData = message;

            IntPtr result = SendMessage(handle, WM_COPYDATA, IntPtr.Zero, ref data);
            return true;
        }

        public static void GetWindowHandleId(int hWnd, out int lpdwProcessId)
        {
            GetWindowThreadProcessId(hWnd, out lpdwProcessId);
        }
        #endregion
    }
}
