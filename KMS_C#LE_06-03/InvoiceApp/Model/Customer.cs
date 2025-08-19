using System;

namespace InvoiceApp.Model
{

    // customer Class
    public class Customer
    {

        //customer ID
        public string Id { get; set; }

        // Customer's name
        public string Name { get; set; }

        // Constructor with initialization
        public Customer(string id, string name)
        {
            Id = id;
            Name = name;
        }

     
        // Display format for UI components
        public override string ToString()
        {
            return $"{Name} - {Id}";
        }
    }
}

