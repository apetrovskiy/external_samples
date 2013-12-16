using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Northwind.Core;

namespace Northwind.Webforms
{
    public partial class Default : System.Web.UI.Page
    {

        private ICustomerRepository repository;

        [Ninject.Inject]
        public void Setup(ICustomerRepository customerRepository)
        {
            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }
            this.repository = customerRepository;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            customersGridView.DataSource = repository.GetAll();
            customersGridView.DataBind();
        }
    }
}