using System.Windows;
using System.Windows.Controls;
using InvoiceApp.ViewModels;

namespace InvoiceApp.Views
{
    public partial class LoginView : UserControl
    {
        // Code-behind for LoginView (UserControl for login form)
        // loaded UI defined in xaml
        public LoginView()
        {
            InitializeComponent();

        }

        // Handles PasswordBox password changes and updates the ViewModel
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Here can check that Datacontext for this view is of LoginViewmodel or not.
            if (this.DataContext is ViewModels.LoginViewModel vm)
            {
                // Sync PasswordBox input with ViewModel property (when is ok svae pass in properta oassword in ViewModel)
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
