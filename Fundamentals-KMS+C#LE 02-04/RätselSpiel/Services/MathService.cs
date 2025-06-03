using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RätselSpiel.Services
{
    internal class MathService
    {
        public readonly Random random = new Random();

        public int MatheRätselBewertung()
        {
            // Es werden zwei Zufallszahlen generiert
            int numberA = random.Next(1, 10);
            int numberB = random.Next(1, 10);
            int richtigeAntwort = 0;
            string frage = "";
            //List<string> optionen = new List<string>();

            // 1=+, 2=-, 3=*, 4=/
            int opration = random.Next(1, 5);
            switch (opration)
            {
                case 1:
                    richtigeAntwort = numberA + numberB;
                    frage = $"{numberA} + {numberB}";
                    break;

                case 2:
                    richtigeAntwort = numberA - numberB;
                    frage = $"{numberA} - {numberB}";
                    break;

                case 3:
                    richtigeAntwort = numberA * numberB;
                    frage = $"{numberA} * {numberB}";
                    break;

                case 4:
                    // Dadurch ergibt die Division immer eine Ganzzahl
                    numberA = numberB * random.Next(1, 10);
                    richtigeAntwort = numberA / numberB;
                    frage = $"{numberA} / {numberB}";
                    break;
            }// End switch


            Console.WriteLine($"\nLösen Sie: {frage} = ?");
            // Eine Variable zum Speichern der Benutzerantwort
            string antwortEingabe = null;

            // Startet eine Hintergrundaufgabe, die auf die Benutzereingabe wartet.
            // Sie wird unabhängig vom Hauptprogramm gestartet, sodass wir ein Zeitlimit setzen können.
            Task task = Task.Run(() => {
                antwortEingabe = Console.ReadLine();
            });

            // Wartet maximal 20 Sekunden (20.000 Millisekunden) auf die Eingabe durch die Task.
            bool wartenZeit = task.Wait(20000);


            if (!wartenZeit)
            {
                Console.WriteLine($"Zeit abgelaufen! Keine Antwort erhalten richtige Antwort ist {richtigeAntwort}.");
                Console.WriteLine("Sie erhalten eine negative Bewertung.");
                return -1;
            }

            if (int.TryParse(antwortEingabe, out int antwort))
            {
                if (antwort == richtigeAntwort)
                {
                    Console.WriteLine("Sie haben Richtig beantwortet");
                    Console.WriteLine("Sie erhalten zwei Pluspunkte.");
                    return 2;

                }
                else
                {
                    Console.WriteLine($"Falsch. Die richtige Antwort ist {richtigeAntwort}.");
                    Console.WriteLine("Sie habe ein negetive Punkt bekommen");
                    return -1;
                }

            }//End if
            else
            {
                Console.WriteLine("Bitte nur Zahlen eingeben.\n");
                Console.WriteLine("Sie habe ein negetive Punkt bekommen");
                return -1;
            }
        }// End StelleMatheAufgabe()

    } // End MathService
}
