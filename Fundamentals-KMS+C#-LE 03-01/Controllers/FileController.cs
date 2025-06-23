using System; // Basisfunktionen wie Console, Exception, String  
using System.Collections.Generic; // Generische Listen und Datenstrukturen wie List<T> 
using System.IO; // Dateioperationen wie Lesen/Schreiben
using Newtonsoft.Json; // JSON-Serialisierung und -Deserialisierung
using Fundamentals_KMS_C__LE_03_01.Models;
using Fundamentals_KMS_C__LE_03_01.Interfaces;
using Fundamentals_KMS_C__LE_03_01.Exceptions;

// Implementiert das Adressbuch mit Datenspeicherung (JSON-Datei)
namespace Fundamentals_KMS_C__LE_03_01.Controllers
{
    internal class FileController : IAddressBook
    {
        // Liste für gespeicherte Adressen im Speicher
        private List<Address> myAddressList;


        // JSON-Dateipfad im gleichen Ordner wie die EXE
        private readonly string filePath = "FileAddress.json";


        // Konstruktor der Klasse – wird beim Erstellen des Objekts automatisch ausgeführt
        public FileController()
        {
            myAddressList = LoadFromFile();

        }



        // Fügt neue Adresse hinzu, wenn Telefonnummer noch nicht existiert
        public void AddAddress(Address address)
        {

            foreach (Address addressItem in myAddressList)
                {
                if (addressItem.PhoneNumber == address.PhoneNumber)
                {
                    throw new DuplicatePhoneNumberException("Diese Telefonnummer gibt es schon.");
                }
                }

                myAddressList.Add(address);
                SaveAddressListToFile();
            }



        // Entfernt Adresse anhand Telefonnummer
        public void RemoveAddress(string phoneNumber)
        {
            Address gefunden = null;

            foreach (Address addressItem in myAddressList)
            {
                if (addressItem.PhoneNumber == phoneNumber)
                {
                    gefunden = addressItem;
                    break;
                }
            }

            if (gefunden != null)
            {
                myAddressList.Remove(gefunden);
                SaveAddressListToFile();
            }
            else
            {
                throw new AdresseNichtGefundenException("Adresse wurde nicht gefunden.");
            }
        }



        // Sucht Adresse mit Telefonnummer
        public Address FindAddress(string phoneNumber)
        {
            foreach (Address addressItem in myAddressList)
            {
                if (addressItem.PhoneNumber == phoneNumber)
                    return addressItem;
            }
            throw new AdresseNichtGefundenException("Adresse wurde nicht gefunden.");

        }



        // Gibt alle Adressen im Speicher zurück
        public List<Address> ShowAllAddresses()
        {
            return myAddressList;
        }


        // Speichert die Liste in JSON-Datei
        private void SaveAddressListToFile()
        {
            try
            {
                // JsonConvert Wandelt die Adressliste in einen formatierten JSON-Text um
                // JSON schön formatiert mit Formatting.Indented schreiben
                // Schreibt den JSON-Text in die Datei auf dem angegebenen Pfad

                string json = JsonConvert.SerializeObject(myAddressList, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                throw new DateiSpeicherException("Fehler beim Speichern der Datei.", ex);
            }
        }



        /* Lädt die gespeicherten Adressen aus der JSON-Datei
         Dadurch ist die Adressliste beim Start sofort einsatzbereit*/
        private List<Address> LoadFromFile()

            {
            // Falls die Datei nicht existiert, wird eine leere Liste zurückgegeben
            if (!File.Exists(filePath))
                {
                    return new List<Address>();
                }

                try
                {
                // JSON-Datei lesen
                // JSON in eine Liste von Adressen umwandeln
                string json = File.ReadAllText(filePath);
                    List<Address> geladeneListe = JsonConvert.DeserializeObject<List<Address>>(json);

                    if (geladeneListe != null)
                    {
                        return geladeneListe;
                    }
                    else
                    {
                        // Wenn das Ergebnis null ist, eine leere Liste zurückgeben
                        return new List<Address>();
                    }
                }
                catch (Exception ex)
                {
                    throw new DateiLadeFehlerException("Fehler beim Laden der Datei: " + ex.Message);
                }
            }

    }
}
