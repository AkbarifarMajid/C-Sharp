using System.Windows;
using GolddiggerGmbh.ViewModels;
using GolddiggerGmbh.Views;   // CustomerDialog
using GolddiggerGmbh.Model;

namespace GolddiggerGmbh.Views
{
    public partial class DashboardWindow : Window
    {
        private readonly DashboardViewModel dashboardViewModel = new DashboardViewModel();

        public DashboardWindow()
        {
            InitializeComponent(); //Initializing composites.
            DataContext = dashboardViewModel; // View to ViewModel bindings
            dashboardViewModel.LoadAll();     // initial data
        }



        // Search uses VM.Search() which reads SearchMode/SearchText from bindings
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!dashboardViewModel.Search(out var err))
                MessageBox.Show(err, "Search", MessageBoxButton.OK, MessageBoxImage.Warning);
        }



        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dashboardViewModel.ClearSearch();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var customerDialog = new CustomerDialog();
            if (customerDialog.ShowDialog() == true)
            {
                if (dashboardViewModel.TryAdd(customerDialog.Customer, out var err))
                    dashboardViewModel.LoadAll();
                else
                    MessageBox.Show(err, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dashboardViewModel.SelectedCustomer == null)
            {
                MessageBox.Show("Select a customer first.", "Edit",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dlg = new CustomerDialog(dashboardViewModel.SelectedCustomer);
            if (dlg.ShowDialog() == true)
            {
                if (dashboardViewModel.TryUpdate(dlg.Customer, out var err))
                    dashboardViewModel.LoadAll();
                else
                    MessageBox.Show(err, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dashboardViewModel.SelectedCustomer == null)
            {
                MessageBox.Show("Select a customer first.", "Delete",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var selectedCustomer = dashboardViewModel.SelectedCustomer;
            var deletConfirmationResult = MessageBox.Show(
                $"Are you sure to delete “{selectedCustomer.FirstName} {selectedCustomer.LastName}” (Id={selectedCustomer.Id})?",
                "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (deletConfirmationResult != MessageBoxResult.Yes) return;

            if (dashboardViewModel.TryDeleteSelected(out var err))
                dashboardViewModel.LoadAll();
            else
                MessageBox.Show(err, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}


