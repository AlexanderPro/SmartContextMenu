using System;
using System.Windows.Forms;
using System.Threading;
using SmartContextMenu.Forms;
using SmartContextMenu.Utils;

namespace SmartContextMenu
{
    static class Program
    {
        private static Mutex _mutex;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            Application.ThreadException += OnThreadException;

            _mutex = new Mutex(false, AssemblyUtils.AssemblyTitle, out var createNew);
            if (!createNew)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            ex = ex ?? new Exception("OnCurrentDomainUnhandledException");
            OnThreadException(sender, new ThreadExceptionEventArgs(ex));
        }

        static void OnThreadException(object sender, ThreadExceptionEventArgs e) =>
            MessageBox.Show(e.Exception.ToString(), AssemblyUtils.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
