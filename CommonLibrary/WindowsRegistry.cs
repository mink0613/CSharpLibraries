using Microsoft.Win32;
using System.Windows.Forms;

namespace CommonLibrary
{
    public class WindowsRegistry
    {
        public static void RegisterStartUp()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.SetValue(Application.ProductName, Application.ExecutablePath);
        }

        public static void UnRegisterStartUp()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            rk.DeleteValue(Application.ProductName, false);
        }
    }
}
