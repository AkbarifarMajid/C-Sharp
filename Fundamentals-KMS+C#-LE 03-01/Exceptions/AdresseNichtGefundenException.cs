//Exceptions Adresse Nicht Gefunden
using System;

namespace Fundamentals_KMS_C__LE_03_01.Exceptions
{
    public class AdresseNichtGefundenException : Exception
    {
        public AdresseNichtGefundenException() { }

        public AdresseNichtGefundenException(string message) : base(message) { }

        public AdresseNichtGefundenException(string message, Exception inner) : base(message, inner) { }
    }
}