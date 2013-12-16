using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Northwind.Wpf.Infrastructure
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(Action<object> action, Func<object, bool> canExecute = null);
    }
}
