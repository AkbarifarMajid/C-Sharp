using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals_KMS_C__LE_03_01.Exceptions
{
    internal class DateiSpeicherException : Exception
    {
        public DateiSpeicherException() { }

        public DateiSpeicherException(string message) : base(message) { }

        public DateiSpeicherException(string message, Exception inner) : base(message, inner) { }

    }
}
