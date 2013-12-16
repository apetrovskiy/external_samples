using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Northwind.Core;

namespace Notrthwind.Winforms
{
    public partial class CustomerForm : Form
    {
        private readonly ICustomerRepository repository;

        public CustomerForm(ICustomerRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this.repository = repository;
            InitializeComponent();

            customerBindingSource.Add(new Customer());
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            customerBindingSource.EndEdit();
            var customer = customerBindingSource.Current as Customer;
            repository.Add(customer);
        }
    }
}
