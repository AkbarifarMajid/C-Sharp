using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using InvoiceApp.Helpers;
using InvoiceApp.Views;

namespace InvoiceApp.ViewModels
{
    // ViewModel for the login window
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string username;
        private string password;


        // Username input
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }



        // Password input
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }



        // Event to notify the UI when property values change
        public event PropertyChangedEventHandler PropertyChanged;


        // Helper method to raise PropertyChanged event automatically
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //=================================================================================


        // Command for the login button
        public ICommand LoginCommand { get; }



        // Executes the login logic
        private void ExecuteLogin()
        {
            try
            {
                string filePath = @"..\..\Data\users.txt";

                // Check if the user file exists
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("User file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Read all users and check for username/password match
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && parts[0] == Username && parts[1] == Password)
                    {
                        //Successful login open dashboard Window
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            var dashboard = new DashboardWindow();
                            dashboard.Show();

                            // Close the current login window
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window.Title == "InvoiceApp Login")
                                {
                                    window.Close();
                                    break;
                                }
                            }
                        });

                        return;
                    }
                }


                // Invalid username or password
                MessageBox.Show("Invalid username or password!", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception ex)
            {
                // Exception occurred during login
                MessageBox.Show("Login error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Checks if login can be executed (fields are not empty)
        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }


        // Constructor: initializes the login command
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }



    }
}
