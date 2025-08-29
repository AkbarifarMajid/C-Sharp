using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GolddiggerGmbh.Model;
using GolddiggerGmbh.Services;
using MySql.Data.MySqlClient;

namespace GolddiggerGmbh.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly ICustomerService customerService = new CustomerService();


        // Grid's data source; UI binds DataGrid.ItemsSource to this collection.
        // ObservableCollection notifies the UI when items are added/removed.
        public ObservableCollection<Customer> Customers { get; private set; }



        public DashboardViewModel()
        {
            Customers = new ObservableCollection<Customer>(); // init once
        }




        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (selectedCustomer == value) return;
                selectedCustomer = value;
                OnPropertyChanged();
                OnPropertyChanged("HasSelection");
            }
        }



        public bool HasSelection 
        {
            get
            { 
                return SelectedCustomer != null;
            } 
        }



        private string searchMode = "All";
        public string SearchMode
        {
            get 
            {
                return searchMode; 
            }
            set 
            { 
                searchMode = value; 
                OnPropertyChanged();
            }
        }



        private string searchText;
        public string SearchText
        {
            get 
            { 
                return searchText; 
            }
            set 
            { 
                searchText = value; OnPropertyChanged(); 
            }
        }




        private string statusText = "Ready";
        public string StatusText
        {
            get 
            { 
                return statusText; 
            }
            set 
            { 
                statusText = value; OnPropertyChanged(); 
            }
        }



        // -------- Load / Search --------
        public void LoadAll()
        {
            Customers.Clear();
            foreach (var customer in customerService.GetAll())
                Customers.Add(customer);

            StatusText = "Loaded " + Customers.Count + " customers";
        }




        public bool Search(out string error)
        {
            error = null;

            // Read mode/text

            string mode = SearchMode;
            if (string.IsNullOrWhiteSpace(mode))
                mode = "All";

            // Read query text never is null
            string queryTxt = SearchText ?? string.Empty;
            queryTxt = queryTxt.Trim();


            // All  just load everything
            if (mode == "All")
            {
                LoadAll();
                return true;
            }


            // Need a query for other modes
            if (string.IsNullOrWhiteSpace(queryTxt))
            {
                error = "Enter a value to search.";
                return false;
            }


            // Clear LIST grid source before putting the result
            Customers.Clear();

            // By Id
            if (mode == "By Id")
            {
                if (!int.TryParse(queryTxt, out int id))
                {
                    error = $"Id with value {queryTxt} must be a number.";
                    Logger.LogError(error);
                    return false;
                }


                var customer = customerService.GetById(id);
                if (customer != null)
                { 
                    Customers.Add(customer);

                    StatusText = $"Found 1 item (Id={id})";
                }

                else 
                { 
                    StatusText = $"No item for Id={id}";
                }
                return true;
            }

            // By Email
            if (mode == "By Email")
            {
                var customer = customerService.GetByEmail(queryTxt);
                if (customer != null) 
                { 
                    Customers.Add(customer); 
                    StatusText = $"Found 1 item (Email={queryTxt})";
                }
                else 
                { 
                    StatusText = $"No item for Email={queryTxt}";
                }
                return true;
            }

            // Fallback (unexpected mode)
            LoadAll();
            return true;
        }



        public void ClearSearch()
        {
            SearchMode = "All";
            SearchText = string.Empty;
            LoadAll();
        }




        // -------------- CRUD  Methods --------------------
        public bool TryAdd(Customer c, out string error)
        {
            error = null;
            try 
            { 
                customerService.Add(c); 
                StatusText = "Customer added."; 
                return true; 
            }

            catch (Exception ex) 
            { 
                error = "Add failed: " + ex.Message;
                Logger.LogException(ex);
                return false; 
            }
        }





        public bool TryUpdate(Customer c, out string error)
        {
            error = null;
            try 
            { 
                customerService.Update(c); 
                StatusText = "Customer updated."; 
                return true; 
            }

            catch (Exception ex) 
            { 
                error = "Update failed: " + ex.Message;
                Logger.LogException(ex);
                return false; 
            }
        }





        public bool TryDeleteSelected(out string error)
        {
            error = null;
            if (SelectedCustomer == null) 
            { error = "Select a customer first."; 
                return false; 
            }

            try 
            { 
                customerService.Delete(SelectedCustomer.Id);
                StatusText = "Customer deleted."; 
                return true; 
            }
            catch (Exception ex) 
            {
                error = "Delete failed: " + ex.Message;
                Logger.LogException(ex);
                return false; 
            }
        }




        // ---- INotifyPropertyChanged ----
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
