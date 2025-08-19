using System.Collections.ObjectModel;

namespace InvoiceApp.ViewModels.DashboardModules
{
 
    // This class handles total, tax, and final price calculations for invoices
    public static class InvoiceCalculator
    {
        // Total excluding tax
        public static decimal CalculateTotal(ObservableCollection<InvoiceItem> items)
        {
            decimal total = 0;
            foreach (var item in items)
                total += item.Total;
            return total;
        }

        // Calculate 20% tax
        public static decimal CalculateTax(decimal totalAmount)
        {
            return totalAmount * 0.2m;
        }

        // Final total with tax
        public static decimal CalculateGrandTotal(decimal totalAmount, decimal taxAmount)
        {
            return totalAmount + taxAmount;
        }
    }
}
