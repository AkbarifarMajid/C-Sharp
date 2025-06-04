using System;
using System.Collections.Generic;

// Diese Klasse repr�sentiert eine logische oder mathematische Frage mit mehreren Antwortm�glichkeiten
namespace R�tselSpiel.Models
{
    public class Frage
    {
        // Alle Property f�r neine Frage und mit get und set kann wert lesen oder eingestellt
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

        // �berpr�ft ob die Antwort des Benutzers richtig ist.
        public bool uberprufAntwort(int benutzerWahl)
        {
            if(benutzerWahl == IndexRichtigeAntwort ) return true;
            else return false;
            
        }
    }
}
