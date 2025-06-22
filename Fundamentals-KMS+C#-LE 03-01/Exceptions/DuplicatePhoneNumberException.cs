//Exceptions Duplicate PhoneNumber
using System;

namespace Fundamentals_KMS_C__LE_03_01.Exceptions
{
    public class DuplicatePhoneNumberException : Exception
    {
        public DuplicatePhoneNumberException() { }

        public DuplicatePhoneNumberException(string message) : base(message) { }

        public DuplicatePhoneNumberException(string message, Exception inner) : base(message, inner) { }
    }
}