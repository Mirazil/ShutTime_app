using System;
using System.Threading;
using System.Windows.Forms;

namespace ShutdownTimerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            using var mutex = new Mutex(true, "ShutdownTimerAppSingleton", out bool isNewInstance);

            if (!isNewInstance)
            {
                MessageBox.Show("Приложение уже запущено и находится в системном трее.", "Shutdown Timer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
