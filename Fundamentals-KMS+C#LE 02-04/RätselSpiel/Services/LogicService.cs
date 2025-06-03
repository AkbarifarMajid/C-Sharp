using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RätselSpiel.Models;

namespace RätselSpiel.Services
{
    public class LogicService
    {
        private readonly List<Frage> alleFragen;

        public LogicService()
        {
            try
            {
                // Adress zur JSON-Datei, die die Logikfragen enthäl
                string logikFragenAddress = "logikfragen.json";

                // Der gesamte Inhalt der JSON-Datei wird als Text gelesen
                string logikFragenText = File.ReadAllText(logikFragenAddress);

                // Add alle JSON-Text in eine Liste von Frage-Objekten
                alleFragen = JsonConvert.DeserializeObject<List<Frage>>(logikFragenText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("beim Laden der JSON-DateiGibt es Problem:");
                Console.WriteLine(ex.Message);
                //Initialisiert eine leere Liste, um Programmabstürze durch  'Null Reference' zu vermeiden
                alleFragen = new List<Frage>(); 
            }
        }

        public int LogikRätselBewertung()
        {
            if (alleFragen == null || alleFragen.Count == 0)
            {
                Console.WriteLine("Keine Fragen verfügbar. Bitte prüfen Sie die JSON-Datei.");
                return 0;
            }

            var random = new Random();

            //Aus der Liste wird eine zufällige Frage ausgewählt.
            var frage = alleFragen[random.Next(alleFragen.Count)];

            Console.WriteLine($"\nLogikrätsel:\n{frage.Text}");

            for (int i = 0; i < frage.Optionen.Count; i++)
            {
                Console.WriteLine($"{i + 1}-> {frage.Optionen[i]}");
            }

            Console.Write("Was ist Ihre Antwort (1, 2, 3): ");

            // Eine Variable zum Speichern der Benutzerantwort
            string antwortEingabe = null;

            // Startet eine Hintergrundaufgabe, die auf die Benutzereingabe wartet.
            // Sie wird unabhängig vom Hauptprogramm gestartet, sodass wir ein Zeitlimit setzen können.
            Task task = Task.Run(() =>
            {
                antwortEingabe = Console.ReadLine();
            });

            //Wartet maximal 30 Sekunden (30.000 Millisekunden) auf die Eingabe durch die Task.
            bool wartenZeit = task.Wait(30000);

            if (!wartenZeit)
            {
                Console.WriteLine("Zeit abgelaufen! Keine Antwort erhalten.\nSie erhalten eine negative Bewertung.");
                Console.WriteLine($"Richtige Antwort wäre: {frage.Optionen[frage.IndexRichtigeAntwort]}");

                return -1;
            }

            if (int.TryParse(antwortEingabe, out int auswahl) && auswahl >= 1 && auswahl <= frage.Optionen.Count)
            {
                //Die Indexe der Liste bei 0 beginnen, wird 1 abgezogen 
                if (frage.uberprufAntwort(auswahl - 1) == true)
                {
                    Console.WriteLine("Richtig beanwortet Sie erhalten zwei Pluspunkte.");
                    return 2;
                }
                else
                {
                    Console.WriteLine($"Falsch beatwortet!. Richtige Antwort war: {frage.Optionen[frage.IndexRichtigeAntwort]}");
                    Console.WriteLine("Sie erhalten eine negative Bewertung.");
                    return -1;
                }
            }
            else
            {
                Console.WriteLine("Falshe Bitte eine Zahl zwischen 1 und 3 eingeben.");
                Console.WriteLine("Sie erhalten eine negative Bewertung.");
                return -1;

            }
        }
    }
}
