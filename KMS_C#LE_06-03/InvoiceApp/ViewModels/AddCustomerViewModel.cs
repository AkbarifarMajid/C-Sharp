using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using InvoiceApp.Helpers;

namespace InvoiceApp.ViewModels
{
    // ViewModel for creating a new customer
    public class AddCustomerViewModel : INotifyPropertyChanged
    {
        // Customer name input
        public string Name { get; set; }
        // Auto-generated customer ID
        public string Id { get; set; }

        // Customer address input
        public string Address { get; set; }



        // The final created customer object
        public CustomerViewModel CreatedCustomer { get; private set; }


        // Action to close the window (true = saved, false = canceled)
        public Action<bool> CloseWindowAction { get; set; }


        // Command for saving the customer
        public ICommand SaveCommand { get; }


        // Command for canceling
        public ICommand CancelCommand { get; }


        // Constructor: initializes commands and generates customer ID
        public AddCustomerViewModel()
        {
            GenerateCustomerId();
            SaveCommand = new RelayCommand(ExecuteSave);
            CancelCommand = new RelayCommand(() => CloseWindowAction?.Invoke(false));
        }


        // Creates a new unique customer ID by checking the file
        private void GenerateCustomerId()
        {
            string filePath = @"..\..\Data\customers.txt";
            int lastNumber = 0;

            // If file exists, read all lines and find the highest number
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 2 && parts[1].StartsWith("KU-"))
                    {
                        if (int.TryParse(parts[1].Substring(3), out int num) && num > lastNumber)
                        {
                            lastNumber = num;
                        }
                    }
                }
            }


            // Generate new ID (increment last number by 1)
            Id = $"KU-{(lastNumber + 1):D4}";
            OnPropertyChanged(nameof(Id));
        }



        // Action executed when Save is clicked
        private void ExecuteSave()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Address))
            {
                System.Windows.MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Create the new customer object
            CreatedCustomer = new CustomerViewModel
            {
                Name = Name,
                Id = Id,
                Address = Address
            };


            // Close window and signal success
            CloseWindowAction?.Invoke(true);
        }



        // Event for notifying UI when a property changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to raise PropertyChanged event
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
