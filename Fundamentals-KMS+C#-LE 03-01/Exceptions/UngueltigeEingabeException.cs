// Exceptions Ungueltige Eingabe
using System;

namespace Fundamentals_KMS_C__LE_03_01.Exceptions
{
    public class UngueltigeEingabeException : Exception
    {
        public UngueltigeEingabeException() { }

        public UngueltigeEingabeException(string message) : base(message) { }

        public UngueltigeEingabeException(string message, Exception inner) : base(message, inner) { }
    }
}