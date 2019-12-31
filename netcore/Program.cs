using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ThunderbirdTray
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var args = Environment.GetCommandLineArgs().ToList();
            args.RemoveAt(0);
            var debug = false;
            foreach (var arg in args)
            {
                if (arg == "--debug")
                {
                    debug = true;
                }
            }

            using (Mutex mutex = new Mutex(false, "Global\\" + TrayBird.Guid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Instance already running");
                    return;
                }

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TrayBird(debug));
            }
        }
    }
}
