using System;

namespace GolddiggerGmbh.Model
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }     
        public string LastName { get; set; }
        public string Street { get; set; }
        public string No { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }


        public Customer() { }

        public Customer(
            string firstName,
            string lastName,
            string street,
            string no,
            string postalCode,
            string city,
            string email,
            string phoneNumber,
            DateTime birthDate
        )
        {
            try
            {
                if (!ValidationHelper.IsValidFirstName(firstName))
                    throw new ArgumentException("name");

                if (!ValidationHelper.IsValidLastName(lastName))
                    throw new ArgumentException("Lastname");

                if (!ValidationHelper.IsValidStreet(street))
                    throw new ArgumentException("street");

                if (!ValidationHelper.IsNumeric(no))
                    throw new ArgumentException("No");

                if (!ValidationHelper.IsNumeric(postalCode))
                    throw new ArgumentException("PostalCode");

                if (!ValidationHelper.IsValidCity(city))
                    throw new ArgumentException("City");

                if (!ValidationHelper.IsValidEmail(email))
                    throw new ArgumentException("Email");

                if (!ValidationHelper.IsValidPhoneNumber(phoneNumber))
                    throw new ArgumentException("Invalid PhonNummber");

                if (!ValidationHelper.IsValidBirthDate(birthDate))
                    throw new ArgumentException("Invalid Date of Birth");

             
                FirstName = firstName;
                LastName = lastName;
                Street = street;
                No = no;
                PostalCode = postalCode;
                City = city;
                Email = email;
                PhoneNumber = phoneNumber;
                BirthDate = birthDate;

                Logger.LogInfo($"Customer completly validated: {firstName} {lastName}");
            }
            catch (Exception ex)
            {
               
                Logger.LogException(ex);
                throw;
            }
        }
    }
}
