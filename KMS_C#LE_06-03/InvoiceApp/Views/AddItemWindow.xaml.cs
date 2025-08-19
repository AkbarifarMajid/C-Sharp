using System.Windows;
using InvoiceApp.ViewModels;

namespace InvoiceApp.Views
{
    /// <summary>
    /// Code-behind for AddItemWindow (connected to AddItemViewModel)
    /// </summary>
    public partial class AddItemWindow : Window
    {
        // Property to return the final created item
        public ItemViewModel ResultItem { get; private set; }

        public AddItemWindow()
        {
            InitializeComponent();

            // If DataContext is AddItemViewModel, define window close behavior
            if (DataContext is AddItemViewModel vm)
            {
                vm.CloseWindowAction = result =>
                {
                    // Assign created item to ResultItem
                    ResultItem = vm.CreatedItem;

                    // Pass the dialog result (true = success, false = cancel)
                    DialogResult = result;

                    // Close the window
                    Close();
                };
            }
        }
    }
}
