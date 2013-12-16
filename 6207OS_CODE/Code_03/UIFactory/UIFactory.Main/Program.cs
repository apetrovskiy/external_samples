using System;
using System.Windows.Forms;
using Ninject;

namespace UIFactory.Main
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

var kernel = new StandardKernel();
kernel.Load("*.dll");

Application.Run(kernel.Get<Form1>());
        }
    }
}
