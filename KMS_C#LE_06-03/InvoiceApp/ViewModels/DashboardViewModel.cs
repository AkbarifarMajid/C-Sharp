using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using InvoiceApp.Helpers;
using InvoiceApp.Views;
using InvoiceApp.ViewModels.DashboardModules;
using System.Collections.Generic;

namespace InvoiceApp.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        //List of customers
        public ObservableCollection<CustomerViewModel> Customers { get; set; } = new ObservableCollection<CustomerViewModel>();

        //  List of items
        public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>();

        //  Items currently added to invoice
        public ObservableCollection<InvoiceItem> InvoiceItems { get; set; } = new ObservableCollection<InvoiceItem>();



        //  Selected customer
        public CustomerViewModel SelectedCustomer { get; set; }

        // Selected item
        public ItemViewModel SelectedItem { get; set; }

        //  Quantity to add (Digit Item)
        public int Quantity { get; set; }



        // Total before tax
        public decimal TotalAmount => InvoiceCalculator.CalculateTotal(InvoiceItems);

        //  Tax amount (20%)
        public decimal TaxAmount => InvoiceCalculator.CalculateTax(TotalAmount);

        // total (total + tax)
        public decimal GrandTotal => InvoiceCalculator.CalculateGrandTotal(TotalAmount, TaxAmount);



        // Commands
        public ICommand AddToInvoiceCommand { get; }
        public ICommand SaveInvoiceCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand AddCustomerCommand { get; }
        public ICommand ExitCommand { get; }


        //  Constructor
        public DashboardViewModel()
        {
            // Define commands
            AddToInvoiceCommand = new RelayCommand(ExecuteAddToInvoice, CanExecuteAddToInvoice);
            SaveInvoiceCommand = new RelayCommand(ExecuteSaveInvoice, CanExecuteSaveInvoice);
            AddItemCommand = new RelayCommand(OpenAddItemWindow);
            AddCustomerCommand = new RelayCommand(OpenAddCustomerWindow);
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());

            // Load file information
            Customers = InvoiceFactory.LoadCustomers(@"..\..\Data\customers.txt");
            Items = InvoiceFactory.LoadItems(@"..\..\Data\items.txt");

            // Update values when changing invoice list
            InvoiceItems.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(TotalAmount));
                OnPropertyChanged(nameof(TaxAmount));
                OnPropertyChanged(nameof(GrandTotal));
                ((RelayCommand)SaveInvoiceCommand).RaiseCanExecuteChanged();
            };
        }

        // Check if saving the invoice is possible
        private bool CanExecuteSaveInvoice()
        {
            return InvoiceItems.Count > 0 && SelectedCustomer != null;
        }

        //
        //Save in Invoice
        private void ExecuteSaveInvoice()
        {
            InvoiceSaver.SaveInvoice(
                path: @"..\..\Data\invoices.txt",
                customer: SelectedCustomer,
                invoiceItems: InvoiceItems.ToList(),
                total: TotalAmount,
                tax: TaxAmount,
                grandTotal: GrandTotal
            );

            InvoiceItems.Clear();
            Quantity = 0;

            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(TotalAmount));
            OnPropertyChanged(nameof(TaxAmount));
            OnPropertyChanged(nameof(GrandTotal));
        }


        // Add the item to the invoice
        private void ExecuteAddToInvoice()
        {
            if (SelectedItem == null || Quantity <= 0)
                return;

            InvoiceItems.Add(new InvoiceItem
            {
                Name = SelectedItem.Name,
                Quantity = Quantity,
                Price = SelectedItem.Price
            });

            Quantity = 0;
            OnPropertyChanged(nameof(Quantity));
        }

        private bool CanExecuteAddToInvoice()
        {
            return SelectedItem != null && Quantity > 0;
        }


        // Open the Add Customer window
        private void OpenAddCustomerWindow()
        {
            var window = new AddCustomerWindow();
            bool? result = window.ShowDialog();

            if (result == true && window.ResultCustomer != null)
            {
                if (Customers.Any(c => c.Id == window.ResultCustomer.Id))
                {
                    MessageBox.Show("The customer ID is duplicated.");
                    return;
                }

                Customers.Add(window.ResultCustomer);
                InvoiceFactory.SaveNewCustomer(window.ResultCustomer);
            }
        }

        // Open the Add Product window
        private void OpenAddItemWindow()
        {
            var window = new AddItemWindow();
            bool? result = window.ShowDialog();

            if (result == true && window.ResultItem != null)
            {
                Items.Add(window.ResultItem);
                InvoiceFactory.SaveNewItem(window.ResultItem);
            }
        }

        // Notify the UI of changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //Invoice item model
    public class InvoiceItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }



        //  Total price for this row
        public decimal Total => Quantity * Price;
    }
}


