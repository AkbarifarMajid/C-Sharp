using System;
using System.Collections.Generic;
using Fundamentals_KMS_C__LE_03_01.Exceptions;
using Fundamentals_KMS_C__LE_03_01.Interfaces;
using Fundamentals_KMS_C__LE_03_01.Models;

// Verantwortlich für die Benutzerschnittstelle über die Konsole (Eingabe/Ausgabe)

namespace Fundamentals_KMS_C__LE_03_01.Views
{
    internal class ConsoleView
    {
        // Konstruktor: erhält eine konkrete Implementierung des Interfaces
        public ConsoleView(IAddressBook controller)
        {
            this.controller = controller;
        }
    

        private IAddressBook controller;

        //----------------------------------------------------------------------------------
        public void AddressProgramm()
        {
            while (true)
            {
                Console.WriteLine("==== Adressverwaltung ====");
                Console.WriteLine("1. Adresse hinzufügen");
                Console.WriteLine("2. Adresse löschen");
                Console.WriteLine("3. Adresse suchen");
                Console.WriteLine("4. Alle Adressen anzeigen");
                Console.WriteLine("0. Beenden");
                Console.Write("Wählen Sie eine Option: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddAddressView();
                            break;
                           
                        case "2":
                            RemoveAddressView();
                            break;

                        case "3":
                            FindAddressView();
                            break;

                        case "4":
                            ShowAllAddressesView();
                            break;

                        case "0":
                            Console.WriteLine("Vielen Dank, dass Sie sich für das Programm entschieden haben.");
                            return;

                        default:
                            throw new UngueltigeEingabeException("Achtung : Ungültige Eingabe FEHLER.");
                    }
                }
                catch (DuplicatePhoneNumberException fehler)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Achtung : (Duplikat FEHLER): " + fehler.Message);
                    Console.ResetColor();
                }
                catch (AdresseNichtGefundenException fehler)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Achtung : (Nicht gefunden FEHLER ): " + fehler.Message);
                    Console.ResetColor();
                }
                catch (UngueltigeEingabeException fehler)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Achtung : (Eingabe FEHLER): " + fehler.Message);
                    Console.ResetColor();
                }
                catch (Exception fehler)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Achtung : UNERWARTETER FEHLER: " + fehler.Message);
                    Console.ResetColor();
                }


                Console.WriteLine(); // Leerzeile
            } // End of while

        } // End AddressProgramm


        // Neue Adresse abfragen und hinzufügen
        private void AddAddressView()
        {
            Address address = new Address();

            Console.Write("Voname: ");
            address.FirstName = Console.ReadLine();

            Console.Write("Nachname: ");
            address.LastName = Console.ReadLine();

            Console.Write("Straße: ");
            address.Street = Console.ReadLine();

            Console.Write("Stadt: ");
            address.City = Console.ReadLine();

            Console.Write("Postleitzahl: ");
            address.PostalCode = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            address.PhoneNumber = Console.ReadLine();

            controller.AddAddress(address);
            Console.WriteLine("Adresse wurde gespeichert.");


        }//End of AddAddress


        // Adresse mit Telefonnummer löschen
        private void RemoveAddressView()
        {
            Console.Write("Telefonnummer der zu löschenden Adresse: ");
            string phone = Console.ReadLine();
            controller.RemoveAddress(phone);
            Console.WriteLine("Adresse wurde gelöscht.");
        }// End of RemoveAddress


        //Adresse mit Telefonnummer suchen und anzeigen
        private void FindAddressView()
        {
            Console.Write("Telefonnummer der gesuchten Adresse: ");
            string phone = Console.ReadLine();
            Address found = controller.FindAddress(phone);
            found.DisplayInfo();
        }// End of FindAddress


        //Alle Adressen anzeigen
        private void ShowAllAddressesView()
        {
            List<Address> list = controller.ShowAllAddresses();

            if (list.Count == 0)
            {
                Console.WriteLine("Keine Adressen gefunden.");
                return;
            }

            foreach (Address address in list)
            {
                address.DisplayInfo();
                
            }
        }// End of ShoeAll

    }// End of ConsoleView

}


