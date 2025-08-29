
using System.Windows;
using System.Windows.Controls;
using GolddiggerGmbh.Model;
using GolddiggerGmbh.ViewModels;

namespace GolddiggerGmbh.Views
{
    public partial class CustomerDialog : Window
    {
        //a maintain an instance of the ViewModel to access its methods.
        private readonly CustomerDialogViewModel viewModelDialog;

        // The built customer the caller will read after OK
        public Customer Customer { get; private set; }


        // Constractor Add
        public CustomerDialog()
        {
            InitializeComponent();
            viewModelDialog = new CustomerDialogViewModel();
            DataContext = viewModelDialog; //set DataContext to bind xaml
        }



        //Constractor Edit
        public CustomerDialog(Customer existing)
        {
            InitializeComponent();
            viewModelDialog = new CustomerDialogViewModel(existing);
            DataContext = viewModelDialog; //set DataContext to bind xaml
        }



        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Customer built; // Customer object ready to save
            string error;  // Error message (if the data is wrong)
            string focus;  // Name of the problematic control for focus

            bool ok = viewModelDialog.TryBuildCustomer(out built, out error, out focus);

            if (ok)
            {
                Customer = built;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(error, "Validation",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // close without saving
        }
    }
}
