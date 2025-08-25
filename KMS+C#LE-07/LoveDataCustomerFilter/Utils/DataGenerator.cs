using System;
using System.Collections.Generic;
using LoveDataCustomerFilter.Models;

namespace LoveDataCustomerFilter.Utils
{

    // Simple sample data generator 
    public static class DataGenerator
    {
        private static readonly Random myRandom = new Random();

        // Generates a list of fake customers
        public static List<Customer> GenerateCustomers(int count)
        {
            var customers = new List<Customer>();

            var names = new[] { "Anna", "Ben", "Clara", "Daniel", "Eva", "Felix", "Greta", "Hans", "Iris", "Jonas" };
            var cities = new[] { "Graz", "Wien", "Linz", "Salzburg", "Innsbruck" };
            var categories = new[] { "Electronics", "Books", "Clothing", "Toys", "Groceries" };

            for (int i = 0; i < count; i++)
            {
                var customer = new Customer
                {
                    Name = $"{names[myRandom.Next(names.Length)]} {GetRandomSurname()}",
                    Age = myRandom.Next(18, 80),
                    City = cities[myRandom.Next(cities.Length)],
                    ProductCategory = categories[myRandom.Next(categories.Length)],
                    OrderDate = GetRandomDate(),
                    OrderValue = Math.Round((decimal)(myRandom.NextDouble() * 500), 2)
                };

                customers.Add(customer);
            }

            return customers;
        }


        // Helper: random surname (German common surnames)
        private static string GetRandomSurname()
        {
            var surnames = new[]
            {
                "Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Meyer",
                "Wagner", "Becker", "Schulz", "Hoffmann", "Schäfer", "Koch",
                "Bauer", "Richter", "Klein", "Wolf", "Schröder", "Neumann",
                "Zimmermann", "Braun", "Krüger", "Hofmann", "Hartmann", "Lange",
                "Werner", "Schmitz", "Krause", "Maier", "Lehmann", "Huber",
                "Kaiser", "Fuchs", "Peters", "Lang", "Scholz", "Keller",
                "Vogel", "Jung", "Hahn", "König", "Schubert", "Kaiser",
                "Sommer", "Graf", "Brandt", "Haas", "Kramer", "Schuster",
                "Seidel", "Gruber"
            };
            return surnames[myRandom.Next(surnames.Length)];
        }



        // Helper: random DateTime in the past 2–3 years
        private static DateTime GetRandomDate()
        {
            var start = new DateTime(2022, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(myRandom.Next(range));
        }
    }
}
