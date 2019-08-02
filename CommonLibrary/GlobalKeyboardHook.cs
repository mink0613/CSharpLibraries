using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace CommonLibrary
{
    /*
     * HOW TO USE
     * kbh = new GlobalKeyboardHook();
       kbh.OnKeyPressed += Kbh_OnKeyPressed;
       kbh.OnKeyUnpressed += Kbh_OnKeyUnpressed;
       kbh.HookKeyboard();
     * */
    public class GlobalKeyboardHook
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, GlobalKeyboardHookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private const int WM_SYSKEYDOWN = 0x0104;

        private const int WM_KEYUP = 0x101;

        private const int WM_SYSKEYUP = 0x105;

        private GlobalKeyboardHookProc _proc;

        private IntPtr _hookID = IntPtr.Zero;

        public delegate IntPtr GlobalKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<Key> OnKeyPressed;

        public event EventHandler<Key> OnKeyUnpressed;

        public GlobalKeyboardHook()
        {
            _proc = HookCallback;
        }

        public void HookKeyboard()
        {
            _hookID = SetHook(_proc);
        }

        public void UnHookKeyboard()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(GlobalKeyboardHookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                
                if (OnKeyPressed != null)
                {
                    OnKeyPressed.Invoke(this, KeyInterop.KeyFromVirtualKey(vkCode));
                }
            }
            else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (OnKeyUnpressed != null)
                {
                    OnKeyUnpressed.Invoke(this, KeyInterop.KeyFromVirtualKey(vkCode));
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
