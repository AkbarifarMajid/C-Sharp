using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals_KMS_C__LE_03_01.Models
{
    //Abstrakte Basisklasse mit gemeinsamen Eigenschaften
    public abstract class Person
    {
        // Vorname und Nachname des Mitarbeiters
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Abstrakte Methode, die in den Kindklassen überschrieben wird
        public abstract void DisplayInfo();

    }
}
