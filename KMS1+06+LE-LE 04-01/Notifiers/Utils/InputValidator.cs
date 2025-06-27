using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMS1_06_LE_LE_04_01.Utils
{
    internal static class InputValidator
    {
        // Check if the numeric input is within the allowed range
        public static bool IsValidMenuOption(string input, int min, int max, out int option)
        {
            if (int.TryParse(input, out option))  // Attempt to convert the user input to an integer
            {
                if (option >= min && option <= max)
                {
                    return true;  
                }
                else
                {
                    return false;  
                }
            }

            option = -1;
            return false;
        }



        // Checks if the email contains exactly one '@', not starting with '@', and doesn't end with a dot.
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            int atIndex = email.IndexOf('@');
            int lastAtIndex = email.LastIndexOf('@');

            // The email must have exactly one @ and not at the beginning of the email
            if (atIndex <= 0 || atIndex != lastAtIndex)
                return false;

            if (email.EndsWith("."))
                return false;

            return true;
        }

      
        // Checks the name input Accepts any characters including letters, numbers, and symbols.
        public static bool IsValidName(string name)
        {
            return name?.Trim().Length > 0;
        }

       
        // Validates if the input string can be converted to a valid DateTime
        public static bool IsValidDate(string input, out DateTime date)
        {
            // Try to parse the input string into a DateTime object
    
            bool isValid = DateTime.TryParse(input, out date);

            return isValid;
        }

        
        //Checks if the input text is not empty or just spaces
        public static bool IsNonEmptyText(string input)
        {
            return input?.Trim().Length > 0;
        }


        // Validate positive integer
        public static bool IsPositiveInteger(string input, out int number)
        {
            if (int.TryParse(input, out number))
            {
                return number > 0;
            }
            number = -1;
            return false;
        }


        // Validate positive decimal number
        public static bool IsPositiveDouble(string input, out double number)
        {
            if (double.TryParse(input, out number))
            {
                return number > 0;
            }
            number = -1;
            return false;
        }
    }
}
