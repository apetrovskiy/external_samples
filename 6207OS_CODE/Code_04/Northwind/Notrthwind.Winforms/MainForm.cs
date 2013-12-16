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
    public partial class MainForm : Form
    {
        private readonly ICustomerRepository repository;
        private readonly IFormFactory formFactory;

        public MainForm(ICustomerRepository repository, IFormFactory formFactory)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (formFactory == null)
            {
                throw new ArgumentNullException("formFactory");
            }
            this.repository = repository;
            this.formFactory = formFactory;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            customerBindingSource.DataSource = repository.GetAll();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            var customerForm = formFactory.Create<CustomerForm>();
            if (customerForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadCustomers();
            }
        }
    }
}
