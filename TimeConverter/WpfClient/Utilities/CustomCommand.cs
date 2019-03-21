using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfClient.Utilities
{
    class CustomCommand : ICommand
    {
        // Private members ........................
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        // Eventns ................................
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Constructors ..............................
        public CustomCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        // Methods ................................
        public bool CanExecute(object parameter)
        {
            bool b = _canExecute == null ? true : _canExecute(parameter);

            return b;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
