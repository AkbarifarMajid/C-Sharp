using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;              // for SortDescription
using System.Linq;
using System.Windows.Data;               // for CollectionViewSource and GroupDescriptions
using System.Windows.Input;
using InvoiceApp.Helpers;                 // RelayCommand
using LoveDataCustomerFilter.Models;
using LoveDataCustomerFilter.Utils;
using LoveDataCustomerFilter.Enums;       // FilterKind

namespace LoveDataCustomerFilter.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Best time to use the order time
        private static readonly DateTime BaseTime = new DateTime(2023, 1, 1);

        // === Base data all of customers (EmptyList) ===
        private List<Customer> myAllCustomers;




        // === Data shown in the DataGrid ===
        private ObservableCollection<Customer> filteredCustomers;

        public ObservableCollection<Customer> FilteredCustomers
        {
            get => filteredCustomers;
            private set
            {
                if (filteredCustomers == value) return;  // If it's the same reference as before, don't unnecessarily notify the UI
                filteredCustomers = value; // Set the new collection reference (Save new Value)
                OnPropertyChanged(nameof(FilteredCustomers)); // Tell UI that ItemsSource has changed
            }
        }





        //  Items for the ComboBox
        // This property returns all values of the enum 
         public Array FilterKinds => Enum.GetValues(typeof(FilterKind));

        // olds the user's selection in the ComboBox
        private FilterKind? selectedFilterKind = null; //type nullable; no default selection at startup
        public FilterKind? SelectedFilterKind
        {
            get => selectedFilterKind;
            set
            {
                if (selectedFilterKind == value) return;// If it's the same reference as before, don't unnecessarily notify the UI
                selectedFilterKind = value;// Set the new collection reference(Save new Value)
                OnPropertyChanged(nameof(SelectedFilterKind));// say to UI the value is changed
            }
        }




        //  LINQ style selection: checked = Method Syntax; unchecked = Query Syntax 
        public bool UseQuerySyntax
        {
            // If UseMethodSyntax is false, Query Syntax is active
            get => !UseMethodSyntax;
            set => UseMethodSyntax = !value;   // Just delegate to the top setter 
        }




        //  LINQ style selection: checked = Method Syntax; unchecked = Query Syntax 
        private bool useMethodSyntax = false;
        public bool UseMethodSyntax
        {
            get => useMethodSyntax;
            set
            {
                if (useMethodSyntax == value) return;// If it's the same reference as before, don't unnecessarily notify the UI
                useMethodSyntax = value;// Set the new collection (Save new Value)
                OnPropertyChanged(nameof(UseMethodSyntax)); // Tell the UI that the UseMethodSyntax property has changed
                OnPropertyChanged(nameof(UseQuerySyntax));// say to UI the value is changed
            }
        }



        // Command bound to the Apply Filter
        public ICommand ApplyFilterCommand { get; }

        public MainViewModel()
        {
            // Create sample data and show all items initially
            myAllCustomers = DataGenerator.GenerateCustomers(30);
            FilteredCustomers = new ObservableCollection<Customer>(myAllCustomers);

            ApplyFilterCommand = new RelayCommand(ApplyFilter);
        }




        private void ApplyFilter()
        {
            // Do nothing if no filter is selected yet
            if (SelectedFilterKind == null)
                return;

            IEnumerable<Customer> resultCustomerList = myAllCustomers;

            if (UseMethodSyntax)
            {

                //        METHOD SYNTAX

                switch (SelectedFilterKind.Value)
                {
                    //  City = Graz
                    case FilterKind.CityGraz:
                        resultCustomerList = myAllCustomers.Where(c => c.City == "Graz");
                        break;

                    //  Age < 30
                    case FilterKind.AgeUnder30:
                        resultCustomerList = myAllCustomers.Where(c => c.Age < 30);
                        break;

                    //  OrderValue > 100
                    case FilterKind.OrderValueOver100:
                        resultCustomerList = myAllCustomers.Where(c => c.OrderValue > 100m);
                        break;

                    //  Category = Electronics
                    case FilterKind.CategoryElectronics:
                        resultCustomerList = myAllCustomers.Where(c => c.ProductCategory == "Electronics");
                        break;

                    //  OrderDate > 01.01.2023
                    case FilterKind.OrderDateAfter20230101:
                        resultCustomerList = myAllCustomers.Where(c => c.OrderDate > BaseTime);
                        break;

                    //  Sort by Name (A–Z)
                    case FilterKind.SortByNameAZ:
                        resultCustomerList = myAllCustomers.OrderBy(c => c.Name);
                        break;


                    //Group by City - Grouping will be turned on in the view after ItemsSource is set.
                    case FilterKind.GroupByCityFlattened:
                        resultCustomerList = myAllCustomers
                                 .OrderBy(c => c.City)
                                 .ThenBy(c => c.Name);
                        break;

                    // Top 3 oldest customers
                    case FilterKind.Top3Oldest:
                        resultCustomerList = myAllCustomers.OrderByDescending(c => c.Age).Take(3);
                        break;

                    default:
                        resultCustomerList = myAllCustomers;
                        break;
                }
            }


            else
            {

                //         QUERY SYNTAX

                switch (SelectedFilterKind.Value)
                {
                    // City = Graz
                    case FilterKind.CityGraz:
                        resultCustomerList = from c in myAllCustomers
                                 where c.City == "Graz"
                                 select c;
          
                        break;

                    //  Age < 30
                    case FilterKind.AgeUnder30:
                        resultCustomerList = from c in myAllCustomers
                                 where c.Age < 30
                                 select c;
                        break;

                    //  OrderValue > 100
                    case FilterKind.OrderValueOver100:
                        resultCustomerList = from c in myAllCustomers
                                 where c.OrderValue > 100m
                                 select c;
                        break;

                    //  Category = Electronics
                    case FilterKind.CategoryElectronics:
                        resultCustomerList = from c in myAllCustomers
                                 where c.ProductCategory == "Electronics"
                                 select c;
                        break;

                    // OrderDate > 01.01.2023
                    case FilterKind.OrderDateAfter20230101:
                        resultCustomerList = from c in myAllCustomers
                                 where c.OrderDate > BaseTime
                                 select c;
                        break;

                    // Sort by Name (A–Z)
                    case FilterKind.SortByNameAZ:
                        resultCustomerList = from c in myAllCustomers
                                 orderby c.Name ascending
                                 select c;
                        break;

                    // Group by City — real group display
                    case FilterKind.GroupByCityFlattened:
                        resultCustomerList = from c in myAllCustomers
                                 orderby c.City, c.Name
                                 select c;
                        break;

                    //Top 3 oldest customers
                    case FilterKind.Top3Oldest:
                        resultCustomerList = (from c in myAllCustomers
                                  orderby c.Age descending
                                  select c).Take(3);
                        break;

                    default:
                        resultCustomerList = myAllCustomers;
                        break;
                }
            }

            // Update ItemsSource for the DataGrid
            FilteredCustomers = new ObservableCollection<Customer>(resultCustomerList);

            // Turn grouping on/off in the view Shown group headers 
            ApplyCityGrouping(SelectedFilterKind == FilterKind.GroupByCityFlattened);
        }

       
        
        // Enable/disable grouping on the CollectionView for FilteredCustomers
        private void ApplyCityGrouping(bool enable)
        {
            var view = CollectionViewSource.GetDefaultView(FilteredCustomers);
            if (view == null) return;

            // Clear any previous groups and sorts
            view.GroupDescriptions.Clear();
            view.SortDescriptions.Clear();

            if (enable)
            {
                // Group by City
                view.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Customer.City)));

                // Sort groups and then items inside each group
                view.SortDescriptions.Add(new SortDescription(nameof(Customer.City), ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription(nameof(Customer.Name), ListSortDirection.Ascending));
            }
        }

       
        
        //  INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
