using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals_KMS_C__LE_03_01.Exceptions
{
    internal class DateiLadeFehlerException : Exception
    {
        public DateiLadeFehlerException(string message) : base(message) { }
    }
}
