using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using InvoiceApp.ViewModels;

namespace InvoiceApp.ViewModels.DashboardModules
{

    //This class handles saving the final invoice to a text file
    public static class InvoiceSaver
    {
        public static void SaveInvoice(string path, CustomerViewModel customer, List<InvoiceItem> invoiceItems, decimal total, decimal tax, decimal grandTotal)
        {
            try
            {
                var lines = new List<string>();

                lines.Add("=================================");
                lines.Add($"Name Customer: {customer?.Name ?? "Unknown"} - {customer?.Id ?? "-"}");
                lines.Add($"Date: {DateTime.Now:dd.MM.yyyy}     Time: {DateTime.Now:HH:mm:ss}");
                lines.Add("");
                lines.Add("User Name: admin");
                lines.Add("");
                lines.Add("List Items:");

                int count = 1;
                foreach (var item in invoiceItems)
                {
                    lines.Add($"{count}. Item: {item.Name}    Digit: {item.Quantity}   Unit: {item.Price:0.00}   Total: {item.Total:0.00}");
                    count++;
                }

                lines.Add("");
                lines.Add($"Tax-free collection:\t€ {total:0.00}");
                lines.Add($"Tax:\t\t€ {tax:0.00}");
                lines.Add($"Total Price:\t€ {grandTotal:0.00}");
                lines.Add("=================================");
                lines.Add("");

                File.AppendAllLines(path, lines, Encoding.UTF8);

                MessageBox.Show("Invoice saved successfully.", "Save Invoice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error saving invoice: {ex.Message}");
            }
        }
    }
}
