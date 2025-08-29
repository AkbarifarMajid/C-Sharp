using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using GolddiggerGmbh.Services;   
using GolddiggerGmbh.Model;      

namespace GolddiggerGmbh.ViewModels
{
    //The View(LoginWindow) calls TryLogin() from its Button Click handler.
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Data access service for users (DAL/DB)
        private readonly IUserService myUserService = new UserService();


        // -------------------- Inputs --------------------

        private string username;
        // Username typed by the user.
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
                UpdateCanLogin();    // enable/disable the Enter button
            }
        }

        
        private string password;

        //Password typed by the user set from PasswordBox in the View
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                UpdateCanLogin();    // enable/disable the Enter button
            }
        }


        // -------------------- Enter button state --------------------

        private bool canLogin;

        // Whether the Enter button should be enabled. Bound to the Button's IsEnabled in XAML.
        public bool CanLogin
        {
            get => canLogin;
            private set
            {
                if (canLogin == value) return; //do not need to update
                canLogin = value;
                OnPropertyChanged();
            }
        }

        //This method enables and disables the CanLogin button.
        private void UpdateCanLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                CanLogin = false;
            else
                CanLogin = true;
        }




        // -------------------- Success event--------------------

        // By successful login so the View can open the dashboard and close the login window.
        public event Action LoginSucceeded;


        // --------------------  Login logic und called by Click --------------------

        // called by the Login button Click handler in the View
        public void TryLogin()
        {
            try
            {
                // Log UserName
                Logger.LogInfo($"Login attempt by '{Username}'");

                //call metod in Service
                var user = myUserService.GetByUserAndPassword(Username, Password);

                if (user != null)
                {
                    Logger.LogInfo($"Login success for '{Username}'");
                    //tell View that the login was successful.
                    LoginSucceeded?.Invoke();
                }
                else
                {
                    Logger.LogWarning($"Login failed for '{Username}'");
                    MessageBox.Show("Incorrect username or password.", "Login failed",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            { 
                    Logger.LogError("ExceptionHandler failed."); 
               
            }
        }

        // -------------------- INotifyPropertyChanged --------------------

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
