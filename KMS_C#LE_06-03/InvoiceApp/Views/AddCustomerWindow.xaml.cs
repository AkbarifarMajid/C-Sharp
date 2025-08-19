using System.Windows;
using InvoiceApp.ViewModels;

namespace InvoiceApp.Views
{

    // Code-behind for AddCustomerWindow (connected to AddCustomerViewModel)

    public partial class AddCustomerWindow : Window
    {
        // Property to return the final created customer
        public CustomerViewModel ResultCustomer { get; private set; }


        public AddCustomerWindow()
        {
            InitializeComponent();

            // Create ViewModel and set it as DataContext
            var viewModel = new AddCustomerViewModel();
            this.DataContext = viewModel;



            // Define behavior for closing the window
            viewModel.CloseWindowAction = result =>
            {
                // Assign created customer to ResultCustomer
                ResultCustomer = viewModel.CreatedCustomer;

                // Pass the dialog result (true = success, false = cancel)
                this.DialogResult = result;

                // Close the window
                this.Close();
            };

        }
    }
}
