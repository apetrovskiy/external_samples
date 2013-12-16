using Ninject;
using System.Windows;
using Northwind.Wpf.Infrastructure;
using Ninject.Extensions.Conventions;

namespace Northwind.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

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

                var mainWindow = kernel.Get<IMainView>();
                mainWindow.Show();
            }
        }
    }
}
