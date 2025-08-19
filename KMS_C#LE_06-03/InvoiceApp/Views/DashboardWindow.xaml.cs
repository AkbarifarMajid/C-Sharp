using System.Windows;
using InvoiceApp.ViewModels;

namespace InvoiceApp.Views
{
    // Code-behind for DashboardWindow (connected to DashboardViewModel)
    public partial class DashboardWindow : Window
    {
        public DashboardWindow()
        {
            InitializeComponent();
            // Set ViewModel as DataContext
            DataContext = new DashboardViewModel(); 
        }
    }
}
