using System;
using System.Collections.Generic;

// Diese Klasse repräsentiert eine logische oder mathematische Frage mit mehreren Antwortmöglichkeiten
namespace RätselSpiel.Models
{
    public class Frage
    {
        // Alle Property für neine Frage und mit get und set kann wert lesen oder eingestellt
        public string Text { get; set; }
        public List<string> Optionen { get; set; }
        public int IndexRichtigeAntwort { get; set; }

        // Konstruktor der Klasse Frage: Initialisiert den Fragetext, die Antwortoptionen und den Index der richtigen Antwort
        public Frage(string FrageText, List<string> AntwortOptionen, int indexRichtigeAntwort)
        {
            Text = FrageText;
            Optionen = AntwortOptionen;
            IndexRichtigeAntwort = indexRichtigeAntwort;
        }

        // Überprüft ob die Antwort des Benutzers richtig ist.
        public bool uberprufAntwort(int benutzerWahl)
        {
            if(benutzerWahl == IndexRichtigeAntwort ) return true;
            else return false;
            
        }
    }
}
