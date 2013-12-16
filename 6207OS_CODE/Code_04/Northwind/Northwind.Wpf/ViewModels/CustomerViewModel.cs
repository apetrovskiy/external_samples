using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Northwind.Core;
using Northwind.Wpf.Infrastructure;

namespace Northwind.Wpf.ViewModels
{
    public class CustomerViewModel : ViewModel
    {
        private readonly ICustomerRepository repository;
        private readonly Customer customer;
        private readonly ICommand saveCommand;
        private bool? dialogResult;

        public CustomerViewModel(ICustomerRepository repository, ICommandFactory commandFactory)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (commandFactory == null)
            {
                throw new ArgumentNullException("commandFactory");
            }
            this.repository = repository;
            this.saveCommand = commandFactory.CreateCommand(Save);
            this.customer = new Customer();
        }

        public Customer Customer
        {
            get
            {
                return customer;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
        }

        public void Save(object paramer)
        {
            repository.Add(Customer);
            DialogResult = true;
        }

        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }
    }
}
