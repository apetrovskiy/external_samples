using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Ninject;

using Ninject.Extensions.Conventions;

namespace Notrthwind.Winforms
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

            using (var kernel = new StandardKernel())
            {
                kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
                                    .SelectAllClasses()
                                    .BindAllInterfaces());

                kernel.Bind(x => x.FromThisAssembly()
                                    .SelectAllInterfaces()
                                    .EndingWith("Factory")
                                    .BindToFactory()
                                    .Configure(c => c.InSingletonScope()));

                var mainForm = kernel.Get<MainForm>();
                Application.Run(mainForm);
            }
        }
    }
}
