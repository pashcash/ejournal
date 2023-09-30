using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace web_journal.ViewModels
{
    public class ViewModelCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;

        public ViewModelCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = null;
        }

        public ViewModelCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return execute == null ? true : this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
