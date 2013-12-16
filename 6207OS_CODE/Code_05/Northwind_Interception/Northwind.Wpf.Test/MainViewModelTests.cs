using NUnit.Framework;
using Ninject;
using Ninject.MockingKernel.Moq;
using Northwind.Core;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.ViewModels;
using Ninject.Extensions.Conventions;

namespace Northwind.Wpf.Test
{
    [TestFixture]
    class MainViewModelTests
    {
        private readonly MoqMockingKernel kernel;

        public MainViewModelTests()
        {
            this.kernel = new MoqMockingKernel();

            kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
                            .SelectAllClasses()
                            .BindDefaultInterfaces());

            kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
                            .SelectAllInterfaces()
                            .EndingWith("Factory")
                            .BindToFactory());
        }

        [TearDown]
        public void TearDown()
        {
            kernel.Reset();
        }


        [Test]
        public void GettingCustomersCallsRepositoryGetAll()
        {
            var repositoryMock = kernel.GetMock<ICustomerRepository>();
            repositoryMock.Setup(r => r.GetAll());

            var sut = kernel.Get<MainViewModel>();

            var customers = sut.Customers;

            repositoryMock.VerifyAll();
        }

        [Test]
        public void ExecutingCreateCustomerCommandShowsCustomerView()
        {
            var customerViewMock = kernel.GetMock<ICustomerView>();
            customerViewMock.Setup(v => v.ShowDialog());

            var sut = kernel.Get<MainViewModel>();

            sut.CreateCustomerCommand.Execute(null);

            customerViewMock.VerifyAll();
        }

    }
}
