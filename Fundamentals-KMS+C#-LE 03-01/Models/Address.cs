using System;


namespace Fundamentals_KMS_C__LE_03_01.Models
{
    // Address erbt von der abstrakten Klasse Person
    public class Address : Person
    {
        // Straße und Stadt und Postleitzahl und Telefonnummer der Adresse
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }



        // Methode zur Anzeige der vollständigen Adressdaten
        public override void DisplayInfo()
        {
        
            Console.ForegroundColor = ConsoleColor.Cyan;

            //int cunter = 1;
           // Console.WriteLine($"----- Nummber {cunter} -----");
            Console.WriteLine($"{"Name",-10}: {FirstName} {LastName}");
            Console.WriteLine($"{"Straße",-10}: {Street}");
            Console.WriteLine($"{"Stadt",-10}: {City}");
            Console.WriteLine($"{"PLZ",-10}: {PostalCode}");
            Console.WriteLine($"{"Telefon",-10}: {PhoneNumber}");
         
            Console.ResetColor();

            Console.WriteLine("-------------------");
        }// End of DisplayInfo
      

        // Methode zur Anzeige der vollständigen Adressdaten mit Nummerierung



    }// End of class Address
   
}
