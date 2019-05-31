using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace @lock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool res;
            Mutex mutex = new Mutex(true, "mutex1", out res);
            if (res == false)
            {
                MessageBox.Show("Одна копия уже запущена. Закрываюсь...");
                //mutex.WaitOne();
            } else Application.Run(new Form1());
            if (res) mutex.ReleaseMutex();
        }
    }
}