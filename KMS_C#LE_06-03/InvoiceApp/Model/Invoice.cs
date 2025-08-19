using System;

namespace InvoiceApp.Model
{

    // Class representing (Candidate) a row of an invoice
    public class Invoice
    {

        // Name of the item
        public string Name { get; set; }


        // Quantity of the item
        public int Quantity { get; set; }

        // Unit price
        public decimal Price { get; set; }


        // Total price
        public decimal Total => Quantity * Price;


        // Constructor
        public Invoice(string name, int quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}

