using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using InvoiceApp.ViewModels;

namespace InvoiceApp.ViewModels.DashboardModules
{
    
    // This class handles loading and saving customers and items to/from file
    public static class InvoiceFactory
    {
        //Save new customers in file
        public static void SaveNewCustomer(CustomerViewModel customer)
        {
            try
            {
                string line = $"{customer.Name}|{customer.Id}|{customer.Address}";
                File.AppendAllLines(@"..\..\Data\customers.txt", new[] { line });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problem saving the customer. : {ex.Message}");
            }
        }



        // Save a new Item in file
        public static void SaveNewItem(ItemViewModel item)
        {
            try
            {
                string line = $"{item.Name}|{item.Id}|{item.Price.ToString(CultureInfo.InvariantCulture)}";
                File.AppendAllLines(@"..\..\Data\items.txt", new[] { line });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problem saving the Item.: {ex.Message}");
            }
        }



        // Lode Customer List from file
        public static ObservableCollection<CustomerViewModel> LoadCustomers(string path)
        {
            var list = new ObservableCollection<CustomerViewModel>();

            try
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        list.Add(new CustomerViewModel
                        {
                            Name = parts[0],
                            Id = parts[1],
                            Address = parts[2]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in reading customers: {ex.Message}");
            }

            return list;
        }



        // Lode Items List from file
        public static ObservableCollection<ItemViewModel> LoadItems(string path)
        {
            var list = new ObservableCollection<ItemViewModel>();

            try
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3 &&
                        decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                    {
                        list.Add(new ItemViewModel
                        {
                            Name = parts[0],
                            Id = parts[1],
                            Price = price
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in reading Items: {ex.Message}");
            }

            return list;
        }
    }
}
