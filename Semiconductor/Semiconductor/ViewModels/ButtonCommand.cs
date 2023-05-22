using System;
using System.Windows.Input;

namespace Semiconductor.ViewModels
{
    public class ButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;

        public ButtonCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
