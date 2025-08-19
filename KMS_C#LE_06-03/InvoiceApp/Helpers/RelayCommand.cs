using System;
using System.Windows.Input;

namespace InvoiceApp.Helpers
{
  
    // Basic command implementation without input parameters.
    // Useful for buttons that just run an Action.

    public class RelayCommand : ICommand
    {

        // The action (method) to execute
        private readonly Action execute;

        // Optional condition to check if the command can run
        private readonly Func<bool> canExecute;

        // Internal event to re-evaluate CanExecute
        private event EventHandler CanExecuteChanged_Internal;




        // Constructor - define execute logic and optional canExecute logic
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        //_________________________________________________________________________________

        // Checks whether the command can run at this moment
        //Überprüft, ob der Befehl ausgeführt werden kann oder nicht.
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }


        // Runs the command
        //Implements the command execution logic.
        public void Execute(object parameter)
        {
            execute();
        }


        // Event that say WPF that CanExecute may have changed
        //Automatic button updates
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChanged_Internal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChanged_Internal -= value;
            }
        }

        // Manually trigger CanExecuteChanged event
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged_Internal?.Invoke(this, EventArgs.Empty);
        }
    }

   




    // Generic command implementation with input parameter.
    // Useful when you need to pass data (of type T) into the command.

    public class RelayCommand<T> : ICommand
    {
        // The action (method) to execute with a parameter
        private readonly Action<T> execute;

        // Optional condition that checks if the command can run with a parameter
        private readonly Predicate<T> canExecute;

        // Internal event to re-evaluate CanExecute
        private event EventHandler CanExecuteChanged_Internal;


        // Constructor - define execute logic and optional canExecute logic
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        // Checks whether the command can run with the given parameter
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }


        // Runs the command with the given parameter
        public void Execute(object parameter)
        {
            execute((T)parameter);
        }


        // Event that notifies WPF that CanExecute may have changed
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChanged_Internal += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChanged_Internal -= value;
            }
        }


        // Manually trigger CanExecuteChanged event
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged_Internal?.Invoke(this, EventArgs.Empty);
        }
    }
}
