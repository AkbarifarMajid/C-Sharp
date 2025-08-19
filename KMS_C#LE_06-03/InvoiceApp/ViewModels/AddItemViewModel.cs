using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using InvoiceApp.Helpers;

namespace InvoiceApp.ViewModels
{

    //ViewModel for AddItemWindow (used to add a new product/item)

    public class AddItemViewModel : INotifyPropertyChanged
    {
        private string nameItem;
        private string idItem;
        private string priceItem;


        // Event to notify the UI when a property value changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Product name
        public string Name
        {
            get => nameItem;
            set { nameItem = value; OnPropertyChanged(nameof(Name)); }
        }


        // Product ID (generated automatically by the system)
        public string Id
        {
            get => idItem;
            set { idItem = value; OnPropertyChanged(nameof(Id)); }
        }


        // Price entered as string (validated later)
        public string Price
        {
            get => priceItem;
            set { priceItem = value; OnPropertyChanged(nameof(Price)); }
        }



        // Commands for save and cancel actions
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // Action to close the window with result (true = saved, false = canceled)
        public Action<bool?> CloseWindowAction { get; set; }

        // The final created item
        public ItemViewModel CreatedItem { get; private set; }


        // Constructor: generates a new ID and initializes commands
        public AddItemViewModel()
        {
            GenerateItemId();
            SaveCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }



        // Generates a unique item ID by checking the file
        private void GenerateItemId()
        {
            string filePath = @"..\..\Data\items.txt";
            int lastNumber = 0;

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 2 && parts[1].StartsWith("IT-"))
                    {
                        string numberPart = parts[1].Substring(3);
                        if (int.TryParse(numberPart, out int num) && num > lastNumber)
                        {
                            lastNumber = num;
                        }
                    }
                }
            }

            int newNumber = lastNumber + 1;
            Id = $"IT-{newNumber:D4}";
        }



        // Confirm command: validates input and creates the item
        private void Confirm()
        {
            // Validate name
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Please enter the product name.");
                return;
            }


            // Validate price
            if (!decimal.TryParse(Price, out decimal priceValue) || priceValue <= 0)
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }


            // Create the item
            CreatedItem = new ItemViewModel
            {
                Name = Name,
                Id = Id,
                Price = priceValue
            };

            // Close window with success
            CloseWindowAction?.Invoke(true);
        }


        // Cancel command: closes the window without saving
        private void Cancel()
        {
            CloseWindowAction?.Invoke(false);
        }


        // Helper method to notify UI about property changes
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
