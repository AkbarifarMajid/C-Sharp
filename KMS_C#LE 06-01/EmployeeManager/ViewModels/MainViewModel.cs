using System.ComponentModel;
using EmployeeManager.Model;

namespace EmployeeManager.ViewModels
{
    // This is the main ViewModel class responsible for holding data and notifying the View of changes
    public class MainViewModel : INotifyPropertyChanged
        //INotifyPropertyChanged can notify the View (user interface) of data changes.
    {

        //Private field for employee's name and employee's age and WelcomMessage
        private Employee employee = new Employee();
        private string welcomeMessage;


        // Public property for Name. Notifies the UI when changed.
        public string Name
        {
            get => employee.Name;
            set
            {
                if (employee.Name != value)
                {
                    employee.Name = value;
                    MyValueToChanged(nameof(Name));
                }
            }
        }


        // Public property for Age Notifies the UI when changed.
        public int Age
        {
            get => employee.Age;
            set
            {
                if (employee.Age != value)
                {
                    employee.Age = value;
                    MyValueToChanged(nameof(Age));
                }
            }

        }


        // Public property for WelcomeMessage Notifies the UI when changed.
        public string WelcomeMessage
        {
            get => welcomeMessage;
            set
            {
                if (welcomeMessage != value)
                {
                    welcomeMessage = value;
                    //nameof returns the name of a variable, property, method, or class as a string.
                    MyValueToChanged(nameof(WelcomeMessage));
                }
            }
        }



        // Validates name and age input, then sets a welcome message or an error.
        public void ActionInput()
        {
            if (string.IsNullOrWhiteSpace(Name) || Age <= 0 || Age >= 130)
            {
                WelcomeMessage = "Please fill in all fields.";
                return;
            }


            WelcomeMessage = $"Welcome, {Name}!  Your age is {Age} years.";
        }



        // Event from INotifyPropertyChanged, triggered when a property changes
        public event PropertyChangedEventHandler PropertyChanged;


        // Helper method to raise property changed event
        protected void MyValueToChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
