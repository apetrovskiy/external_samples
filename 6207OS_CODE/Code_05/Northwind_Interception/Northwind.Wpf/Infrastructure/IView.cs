using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Wpf.Infrastructure
{
    public interface IView
    {
        bool? ShowDialog();

        void Show();

        void Close();

        bool? DialogResult { get; set; }
    }
}
