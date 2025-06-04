using System; // alle basic classe wie(Date Time, Exception,Math,Console)
using System.Collections.Generic; // fur die generic class wie (List< >, dictionary <k,v> ...)
using System.IO; // für die file bearbeiten wie( File.ReadAllText or File.WriteAllText ...) json 
using System.Threading.Tasks; // asynchroner Programmierung und Task Aufgabe(nicht in gleiche zeit)
using Newtonsoft.Json; //Wird verwendet, um JSON ↔ Klasse C# zu konvertieren.(JsonConvert.DeserializeObject und JsonConvert.SerializeObject)

using RätselSpiel.Models; //Importiert den Namespace  um auf die Klasse Frage.cs zugreifen zu können


namespace RätselSpiel.Services
{
    public class LogicService
    {

        // Liste von Objekten zum gelesenen Frahe halten
        private readonly List<Frage> alleFragen;

        public LogicService()
        {
            try
            {
                // Adress zur JSON-Datei, die die Logikfragen enthäl
                string logikFragenAddress = "logikfragen.json";

                // Der gesamte Inhalt der JSON-Datei wird als Text(als String) gelesen
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

        //Metode führt den gesamten Prozess (Fragen stellen, Antworten erhalten und Bewertens)
        public int LogikRätselBewertung()
        {
            if (alleFragen == null || alleFragen.Count == 0)
            {
                Console.WriteLine("Keine Fragen verfügbar. Bitte prüfen Sie die JSON-Datei.");
                return 0;
            }

            Random random = new Random();

            //Aus der Liste wird eine zufällige Frage ausgewählt.
            Frage ausgewählteFrage = alleFragen[random.Next(alleFragen.Count)];

            Console.WriteLine($"\n{ausgewählteFrage.Text}");

            // Schleife zum Durchlaufen aller Antwortoptionen der ausgewählten Frage
            for (int i = 0; i < ausgewählteFrage.Optionen.Count; i++)
            {
                Console.WriteLine($"{i + 1}- {ausgewählteFrage.Optionen[i]}");
            }

            Console.Write("Was ist Ihre Antwort (1, 2, 3): ");

            // Eine Variable zum Speichern der Benutzerantwort
            string antwortEingabe = null;

            // Startet eine Hintergrundaufgabe, die auf die Benutzereingabe wartet.
            // Sie wird unabhängig vom Hauptprogramm gestartet, sodass wir ein Zeitlimit setzen können.
            Task eingabeWartenTask = Task.Run(() =>
            {
                antwortEingabe = Console.ReadLine();
               
            });

            //Wartet maximal 30 Sekunden (30.000 Millisekunden) auf die Eingabe durch die Task.(True oder false)
            bool wartenZeit = eingabeWartenTask.Wait(30000);

            if (!wartenZeit == true)
            {
                Console.WriteLine("\nZeit abgelaufen! Keine Antwort erhalten.\nSie erhalten eine negative Bewertung.");

                Console.WriteLine($"Falsch beantwortet. Richtige Antwort wäre: {ausgewählteFrage.IndexRichtigeAntwort + 1}- {ausgewählteFrage.Optionen[ausgewählteFrage.IndexRichtigeAntwort]}");
                return -1;
            }

            // TryParse => versucht convert eingabe to int (t,f)
            if (int.TryParse(antwortEingabe, out int auswahl) && auswahl >= 1 && auswahl <= ausgewählteFrage.Optionen.Count)
            {
                //Die Indexe der Liste bei 0 beginnen, wird 1 abgezogen 
                if (ausgewählteFrage.uberprufAntwort(auswahl - 1) == true)
                {
                    Console.WriteLine("Richtig beanwortet Sie erhalten zwei Pluspunkte.");
                    return 2;
                }
                else
                {
                    Console.WriteLine($"Falsch beantwortet. Richtige Antwort wäre: {ausgewählteFrage.IndexRichtigeAntwort + 1}- {ausgewählteFrage.Optionen[ausgewählteFrage.IndexRichtigeAntwort]}");
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
