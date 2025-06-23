using System;
using Fundamentals_KMS_C__LE_03_01.Controllers;
using Fundamentals_KMS_C__LE_03_01.Exceptions;
using Fundamentals_KMS_C__LE_03_01.Interfaces;
using Fundamentals_KMS_C__LE_03_01.Views;


// Diese Klasse zeigt das Hauptmenü an und steuert die Auswahl des Controllers.
namespace Fundamentals_KMS_C__LE_03_01.Controllers
{
    internal class MainMenu

    {
        public void Start ()
        {
            IAddressBook controller = null;

            // Hauptmenü-Schleife mit Fehlerbehandlung
            while (true)
            {
                try
                {
                    Console.WriteLine("Möchten Sie mit Datei oder Console arbeiten ?");
                    Console.WriteLine("1. Console (ConsloeController)");
                    Console.WriteLine("2. File (FileController)");
                    Console.WriteLine("x. Beenden");
                    Console.Write("Ich Wähle : ");
                    string input = Console.ReadLine();

                    if (input == "x" || input == "X")
                    {
                        Console.WriteLine("Vielen Dank, dass Sie sich für das Programm entschieden haben.");
                        return;
                    }

                    if (input == "1")
                    {
                        controller = new ConsloeController();
                        break;
                    }
                    else if (input == "2")
                    {
                        controller = new FileController();
                        break;
                    }
                    else
                    {
                        // Benutzer hat eine ungültige Eingabe gemacht – benutzerdefinierte Exception auslösen
                        throw new UngueltigeEingabeException("Achtung : Bitte wählen Sie nur 1, 2 oder x.");
                    }
                }
                // Behandelt gezielt ungültige Benutzereingaben
                catch (UngueltigeEingabeException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("FEHLER (Eingabe): " + ex.Message);
                    Console.ResetColor();
                }
                // Behandelt unerwartete Fehler
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("UNERWARTETER FEHLER: " + ex.Message);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            // Nach erfolgreicher Auswahl → Starte die Benutzeroberfläche
            ConsoleView view = new ConsoleView(controller);
            view.AddressProgramm();

        }
    }
}
