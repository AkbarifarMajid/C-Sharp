using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals
{
    internal class Person
    {
        public String Vorname { get; set; }
        public String Nachname { get; set; }
        public int PersonAlter { get; set; }
        public void Display()
        {
            Console.WriteLine($"Name: {Vorname} {Nachname}, Age: {PersonAlter}\n");
        }
    }// End of Person class
}
