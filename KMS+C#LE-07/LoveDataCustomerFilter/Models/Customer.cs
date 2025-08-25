using System;

namespace LoveDataCustomerFilter.Models
{
    // Represents a customer with order details
    public class Customer
    {
        // Full name of the customer
        public string Name { get; set; }

        // Age of the customer
        public int Age { get; set; }

        // City where the customer lives
        public string City { get; set; }

        // Product category ordered by the customer
        public string ProductCategory { get; set; }

        // Date when the order was placed
        public DateTime OrderDate { get; set; }

        // Value of the order in Euros
        public decimal OrderValue { get; set; }
    }
}
