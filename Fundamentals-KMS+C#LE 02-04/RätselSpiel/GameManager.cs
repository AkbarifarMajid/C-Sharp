using System;
using RätselSpiel.Services;

namespace RätselSpiel
{
    public class GameManager
    {
        // Gesamtpunktzahl, die während des Spiels gesammelt wird.
        private int gesamtPunkte = 0;

        // Dienste für Mathe- und Logikaufgaben werden einmal erstellt
        private readonly MathService mathService = new MathService();
        private readonly LogicService logicService = new LogicService();

        // Hauptmethode, um das Spiel zu starten
        public void StartSpiel()
        {
            Console.WriteLine("Willkommen bei BrainTech Games!");
            bool neuSpielen = true;

            while (neuSpielen)
            {
               
                Console.WriteLine("\nIn welche Kategorie wollen Sie spielen?");
                Console.WriteLine("1 - Mathematische Rätsel");
                Console.WriteLine("2 - Logische Rätsel");
                Console.WriteLine("X - Spiel beenden");

                string benutzerEingabe = Console.ReadLine();
                switch (benutzerEingabe)
                {
                    case "1":
                        SpieleMatheFragen();
                        break;

                    case "2":
                        SpieleLogikFragen();
                        break;

                    case "x":
                        neuSpielen = false;
                        break;

                    // Bei falscher Eingabe erneut fragen
                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        continue;
                }

                // Punktestand anzeigen
                Console.WriteLine($"\nAktuelle Punktzahl: {gesamtPunkte}");

                Console.Write("Möchten Sie weiterspielen? (j/n): ");
                string wahl = Console.ReadLine();

                if (wahl?.ToLower() != "j")
                    neuSpielen = false;
            }

            Console.WriteLine($"\nSpiel beendet! Ihre Gesamtpunktzahl: {gesamtPunkte}");
        }

        // Metode zum Mathe-Spiel
        private void SpieleMatheFragen()
        {
            while (true)
            {
          
                Console.WriteLine("\nMathematische Aufgabe:");

                //
                int punkte = mathService.MatheRätselBewertung();
                gesamtPunkte += punkte;

                Console.WriteLine($"Aktuelle Punktzahl: {gesamtPunkte}");


                Console.Write("\nDrücken Sie Enter für eine neue Aufgabe oder geben Sie 'x' ein zum Zurückgehen: ");
                string eingabe = Console.ReadLine()?.ToLower();

                if (eingabe == "x")
                    break;
            }
        }


        // Metode zum Logisch Spielen
        private void SpieleLogikFragen()
        {
            while (true)
            {
                Console.WriteLine("\nLogikrätsel:");

                //
                int punkte = logicService.LogikRätselBewertung();
                gesamtPunkte += punkte;

                Console.WriteLine($"Aktuelle Punktzahl: {gesamtPunkte}");

                Console.Write("\nDrücken Sie Enter für ein neues Rätsel oder geben Sie 'x' ein zum Zurückgehen: ");
                string eingabe = Console.ReadLine()?.ToLower();

                if (eingabe == "x")
                    break;
            }
        }

    }
}
