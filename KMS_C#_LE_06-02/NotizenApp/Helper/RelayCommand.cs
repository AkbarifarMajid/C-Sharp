using System;
using System.Windows.Input;

namespace NotizenApp.Helper


{

    // This class implements commands that can be bound to buttons and other controls (With out input Parameter)
    public class RelayCommand : ICommand
    {

        // The main method that is apply when the command is executed
        private readonly Action execute;

        // A function that control or this command is executable(can run) or not
        private readonly Func<bool> canExecute;

        // Class constructor: initializes the execution method and execution condition
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }



        // Checks that the command is executable(can run) in the running state or not
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        // Run the command (calls the asked method)
        public void Execute(object parameter)
        {
            execute();
        }

        // This event is called when the UI needs to check if the command is active or not
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }









    // Generic (Parameterized) version for commands that take with input parameters
    public class RelayCommand<T> : ICommand
    {
        // Method to be executed (run) with a parameter of type T
        private readonly Action<T> execute;

        // A function that control or this command is executable(can run) or not
        private readonly Predicate<T> canExecute;

        // Class constructor: initializes the execution method and execution condition
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        // Checks that the command is executable(can run) in the running state or not
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }


        // Run the command with the given parameter
        public void Execute(object parameter)
        {
            execute((T)parameter);
        }


        // This event is called when the UI needs to check if the command is active or not
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
