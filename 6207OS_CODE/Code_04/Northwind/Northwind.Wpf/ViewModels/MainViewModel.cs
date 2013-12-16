using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Northwind.Core;
using Northwind.Wpf.Infrastructure;
using Northwind.Wpf.Views;

namespace Northwind.Wpf.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ICustomerRepository repository;
        private readonly IViewFactory viewFactory;

        private readonly ICommand createCustomerCommand;

        public MainViewModel(ICustomerRepository repository, IViewFactory viewFactory, ICommandFactory commandFactory)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (viewFactory == null)
            {
                throw new ArgumentNullException("viewFactory");
            }
            if (commandFactory == null)
            {
                throw new ArgumentNullException("commandFactory");
            }
            this.repository = repository;
            this.viewFactory = viewFactory;

            createCustomerCommand = commandFactory.CreateCommand(CreateCustomer);
        }

        public IEnumerable<Customer> Customers
        {
            get { return repository.GetAll(); }
        }

        private void CreateCustomer(object param)
        {
            var customerView = viewFactory.CreateView<ICustomerView>();
            if (customerView.ShowDialog() == true)
            {
                // Refresh the list
                OnPropertyChanged("Customers");
            }
        }

        public ICommand CreateCustomerCommand
        {
            get { return createCustomerCommand; }
        }

    }
}
