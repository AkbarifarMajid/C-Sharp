using System.Windows;
using System.Windows.Controls;
using GolddiggerGmbh.ViewModels;

namespace GolddiggerGmbh.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // Attach the ViewModel and listen for success to open the dashboard
            var myViewModel = new LoginViewModel(); //make viewModel (with Username/Password/CanLogin/TryLogin)
            myViewModel.LoginSucceeded += OnLoginSucceeded;  // listen to the login success event to open the dashboard later.
            DataContext = myViewModel;   // Connects the ViewModel to the window so that the Bindings work.
        }


        // Keep the ViewModel's Password in sync with the PasswordBox
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var myViewModel = DataContext as LoginViewModel;  //Trying to get ViewModel from DataContext
            var myDatabase = sender as PasswordBox;  // Try to get the PasswordBox itself from the sender
            if (myViewModel == null || myDatabase == null) return;

            myViewModel.Password = myDatabase.Password;
        }



        // Button Click call the ViewModel's login method
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel myViewModel)
                myViewModel.TryLogin();
        }


        // On successful login open dashboard and close this window
        private void OnLoginSucceeded()
        {
            var dash = new DashboardWindow();
            dash.Show();
            this.Close();
        }
    }
}
