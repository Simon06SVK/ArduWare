using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ArduWare
{
    internal static class WinSparkle
    {
        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_init();

        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_cleanup();

        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_check_update_with_ui();

        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_appcast_url(string url);

        [DllImport("WinSparkle.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void win_sparkle_set_app_details(string companyName, string appName, string appVersion);
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WinSparkle.win_sparkle_set_app_details("Šimon Maňko", "ArduWare", "1.0");
            WinSparkle.win_sparkle_set_appcast_url("https://raw.githubusercontent.com/Simon06SVK/ArduWare/refs/heads/main/appcast.xml");
            WinSparkle.win_sparkle_init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*try
            {
                string updaterPath = "ArduWareUpdater.exe";
                var updater = Process.Start(updaterPath);
                updater?.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Zlyhala kontrola aktualizacii: {ex.Message}");
            }*/

            Login loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainUI());
            }
        }
    }
}
