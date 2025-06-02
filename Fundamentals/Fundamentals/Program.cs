using System;
using System.Collections.Generic;
using System.Linq;


namespace Fundamentals
{
    internal class Program
    {
        static List<Person> personList = new List<Person>();
        const int CountMaxPeople = 10;
        static void Main(string[] args)
        {
            bool flagExit = false;
            while (!flagExit)
            {
                myMenu();
                Console.Write("\n-------------\n");
                string userInput = Console.ReadLine();

                switch(userInput)
                {
                    case "1":
                        AddPerson();
                        break;

                    case "2":
                        PersonUnter30();
                        break;
                    case "3":
                        AllePeronen();
                        break;
                    case "4":
                        flagExit = true;
                        break;
                    default:
                        Console.WriteLine("Bitte versuchen Sie es erneut.");
                        break;
                }

            }// End of while loop


            // metodes
             void myMenu()
            {
                Console.WriteLine("1. Neu Person");
                Console.WriteLine("2. Alle Personen unter 30 Jahre");
                Console.WriteLine("3. Alle Personen");
                Console.WriteLine("4. Beenden");
                Console.Write("Select an option: ");
            }// End of myMenu method

            void AddPerson()
            {
                while (true)
                {
                    if (personList.Count >= CountMaxPeople)
                    {
                        Console.WriteLine("Maximale Anzahl von Personen erreicht.");
                        break;
                    }

                    Console.WriteLine("Neue Person hinzufügen oder 'X' zum Beenden");
                    Console.WriteLine("----------------------");

                    Console.Write("Geben Sie Vorname: ");
                    string vorname = Console.ReadLine();
                    if (vorname.ToUpper() == "X") break;

                    Console.Write("Geben Sie Nachname: ");
                    string nachname = Console.ReadLine();
                    if (nachname.ToUpper() == "X") break;

                    Console.Write("Geben Sie Alter: ");
                    string alt = Console.ReadLine();
                    if (alt.ToUpper() == "X") break;

                    if (int.TryParse(alt, out int alter))
                    {
                        Person person = new Person
                        {
                            Vorname = vorname,
                            Nachname = nachname,
                            PersonAlter = alter
                        };

                        personList.Add(person);
                        Console.WriteLine("Person gespeichert.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Alter muss eine Zahl sein. Die Daten von {vorname} {nachname} konnten nicht hinzugefügt werden.\n");
                    }
                }//End of while loop in AddPerson method
            }//End of AddPerson method


            void PersonUnter30(){
                var listUnter30 = personList.Where(p => p.PersonAlter < 30).ToList();
                if(listUnter30.Count > 0)
                {
                    Console.WriteLine("Personen unter 30 Jahren:");
                    foreach (var person in listUnter30)
                    {
                        person.Display();
                    }
                }
                else
                {
                    Console.WriteLine("Keine Personen unter 30 Jahren gefunden.");
                }
            }// End of PersonUnter30 method

            void AllePeronen()
            {
                if (personList.Count > 0)
                {
                    Console.WriteLine("Alle Personen:");
                    foreach (var person in personList)
                    {
                        person.Display();
                    }
                }
                else
                {
                    Console.WriteLine("Keine Personen vorhanden.");
                }
            }// End of AllePeronen method

        }//End of Main method

    }// End of Program class
}
