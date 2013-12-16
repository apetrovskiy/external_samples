using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Northwind.Wpf.Infrastructure
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Func<object, bool> canExecute = p => true;

        public ActionCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action = action;

            if (canExecute != null)
            {
                this.canExecute = canExecute;
            }
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
