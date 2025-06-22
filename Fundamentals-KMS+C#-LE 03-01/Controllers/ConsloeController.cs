using System;
using System.Collections.Generic;
using Fundamentals_KMS_C__LE_03_01.Interfaces;
using Fundamentals_KMS_C__LE_03_01.Models;
using Fundamentals_KMS_C__LE_03_01.Exceptions;

// Implementiert das Adressbuch mit In-Memory-Verwaltung ohne Datenspeicherung
namespace Fundamentals_KMS_C__LE_03_01.Controllers
{
    public class ConsloeController : IAddressBook
    {
        //Liste zur Speicherung der Adressen
        private List<Address> myAddressList = new List<Address>();

        //  eine neue Adresse hinzu (mit Duplikatsprüfung)
        public void AddAddress(Address address)
        {
            foreach (Address addressItem in myAddressList)
            {
                if (addressItem.PhoneNumber == address.PhoneNumber)
                {
                    //throw new Exception("Diese Telefonnummer gibt es schon.");
                    throw new DuplicatePhoneNumberException("Diese Telefonnummer gibt es schon.");

                }

            }
            myAddressList.Add(address);
 
        }// End of AddAddress


        // Eine Addresse Löschen
        public void RemoveAddress(String phoneNumber)
        {
            Address suchAddress = null;

            foreach (Address addressItem in myAddressList)
            {
                if (addressItem.PhoneNumber == phoneNumber)
                {
                    suchAddress = addressItem;
                    break;
                }
            }

            if (suchAddress != null)
            {
                myAddressList.Remove(suchAddress);
            }
            else
            {
                //throw new Exception("Adresse wurde nicht gefunden.");
                throw new AdresseNichtGefundenException("Adresse wurde nicht gefunden.");
               
            }

        } // End of RemoveAddress


        // Sucht eine Addresse 
        public Address FindAddress(String phoneNumber)
        {
            foreach (Address addressItem in myAddressList)
            {
                if (addressItem.PhoneNumber == phoneNumber)
                {  
                    return addressItem; 
                }
            }
            throw new AdresseNichtGefundenException("Adresse wurde nicht gefunden.");

        }// Enf of FindAddress



        // Gibt alle gespeicherten Adressen zurück
        public List<Address> ShowAllAddresses()
        {
            return myAddressList;
        }

    }

}
