
using System.Windows;

using EmployeeManager.ViewModels;

namespace EmployeeManager
{

    // Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        // constructor method - MainWindow()
        public MainWindow()
        {
            //Launching the user interface from a XAML file (generator of all graphical elements)
            InitializeComponent();
        }

        // Handles button click and calls the input-processing (ActionInput)method from the ViewModel.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.ActionInput();
            }
        }
    }
}
