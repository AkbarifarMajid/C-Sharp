using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace GolddiggerGmbh.Model
{
    public static class ValidationHelper
    {
        // ------------- Base Metods -------------

        public static bool IsStringNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsStringLengthBetween(string input, int minLength, int maxLength)
        {
            if (input == null) return false;
            return input.Length >= minLength && input.Length <= maxLength;
        }

        public static bool IsAlphabetic(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            // Unicode letters
            return Regex.IsMatch(input, @"^\p{L}+$");
        }

        public static bool IsAlphanumeric(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            // Ltter und number
            return Regex.IsMatch(input, @"^[\p{L}\d]+$");
        }

        public static bool IsNumeric(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input, @"^\d+$");
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // email
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            // Phone Number
            var pattern = @"^\+?[0-9\s\-]{10,20}$";
            return Regex.IsMatch(phone, pattern);
        }



        public static bool IsValidStreet(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            // Letters (all languages), digits, space, dot, hyphen, apostrophe, slash
            return Regex.IsMatch(value, @"^[\p{L}\d\s\.\-'/]{2,100}$");
        }

        public static bool IsValidCity(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            // Letters, space, dot, hyphen, apostrophe
            return Regex.IsMatch(value, @"^[\p{L}\s\.\-']{2,100}$");
        }


        public static bool IsValidDate(string date, string format = "yyyy-MM-dd")
        {
            if (string.IsNullOrWhiteSpace(date)) return false;
            return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static bool IsNonNegativeDecimal(decimal value)
        {
            return value >= 0;
        }

        public static bool IsInRange<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value == null) return false;
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public static bool MatchesRegex(string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input, pattern);
        }

        // -------------Special methods -------------

        public static bool IsValidUserName(string userName)
        {
            //userName
            return IsStringLengthBetween(userName, 3, 40) && IsAlphanumeric(userName);
        }
        public static bool IsValidFirstName(string firstName)
        {
            // userName with 2 Big Letter
            return IsStringLengthBetween(firstName, 2, 50) && IsAlphabetic(firstName);
        }

        public static bool IsValidLastName(string lastName)
        {
            //name
            return IsStringLengthBetween(lastName, 2, 50) && IsAlphabetic(lastName);
        }

        public static bool IsValidFullName(string fullName)
        {
            //// Minimum 2 characters, maximum and apaces
            if (!IsStringLengthBetween(fullName, 2, 100)) return false;
            return Regex.IsMatch(fullName, @"^[\p{L} ]+$");
        }

        public static bool IsValidBirthDate(DateTime birthDate)
        {
            var today = DateTime.Today;
            var minDate = today.AddYears(-120);
            return birthDate <= today && birthDate >= minDate;
        }

        public static bool IsValidSalary(decimal salary)
        {
            // salery canot negative
            return salary >= 0 && salary <= 1_000_000_000;
        }

        public static bool IsValidISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;
            //Simple template for ISBN-10 or ISBN-13
            var pattern = @"^(97(8|9))?\d{9}(\d|X)$";
            return Regex.IsMatch(isbn.Replace("-", ""), pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidLicensePlate(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate)) return false;
            //Simple car license plate number pattern 
            var pattern = @"^\d{2}[A-Z]{1}\d{3}$";
            return Regex.IsMatch(plate.ToUpper(), pattern);
        }

        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }


        // Method to check password (example: at least 8 characters, at least one uppercase letter, one number)
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < 8) return false;
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            if (!Regex.IsMatch(password, @"\d")) return false;
            return true;
        }

       
    }
}