using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fundamentals_KMS_C__LE_03_01.Models;

namespace Fundamentals_KMS_C__LE_03_01.Interfaces
{
    //interface classe für Adressverwaltungsklasse
    internal interface IAddressBook
    {
        // Fügt eine neue Adresse hinzu
        void AddAddress(Address address);

        // Entfernt eine Adresse anhand der Telefonnummer
        void RemoveAddress(string phoneNumber);

        // Sucht eine Adresse anhand der Telefonnummer
        Address FindAddress(string phoneNumber);

        // Interfaces/IAddressBook.cs
        List<Address> ShowAllAddresses();
    }
}
