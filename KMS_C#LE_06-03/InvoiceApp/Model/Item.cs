
using System;

namespace InvoiceApp.Model
{

    // Class an item/product
    public class Item
    {
        // Item name
        public string Name { get; set; }


        // Unit price
        public decimal Price { get; set; }


        // Constructor with initialization
        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // Display format ( TextBox - ComboBox ...)
        public override string ToString()
        {
            return $"{Name} - {Price} Euro";
        }
    }
}
