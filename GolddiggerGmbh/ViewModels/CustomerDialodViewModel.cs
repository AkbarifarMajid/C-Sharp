using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GolddiggerGmbh.Model; 

namespace GolddiggerGmbh.ViewModels
{
  
    /// VM for CustomerDialog  Holds title and all input fields - validation - model building.
   
    public class CustomerDialogViewModel : INotifyPropertyChanged
    {
        // ---- Title ----
        private string title = "Add Customer";
        public string Title
        {
            get 
            { return title;
            }
            set 
            { title = value; 
                OnPropertyChanged(); 
            }
        }

        // ---- Inputs and bound to XAML ----
        public int Id { get; set; } // only used in Edit mode


        private string firstName;
        public string FirstName 
        { get => firstName; set 
            { firstName = value; 
                OnPropertyChanged(); 
            } 
        }


        private string lastName;
        public string LastName 
        { get => lastName; 
            set { lastName = value; 
                OnPropertyChanged(); 
            } 
        }

        private string street;
        public string Street 
        { get => street;
            set 
            { street = value;
                OnPropertyChanged(); 
            }
        }

        private string no;
        public string No 
        { get => no; 
            set { no = value;
                OnPropertyChanged(); 
            }
        }

        private string postalCode;
        public string PostalCode 
        { get => postalCode; 
            set { postalCode = value;
                OnPropertyChanged();
            } 
        }

        private string city;
        public string City 
        { get => city; set 
            { city = value; 
                OnPropertyChanged(); 
            } 
        }

        private string email;
        public string Email 
        { get => email; 
            set { email = value; 
                OnPropertyChanged(); 
            } 
        }

        private string phone;
        public string Phone 
        { get => phone; 
            set { phone = value; 
                OnPropertyChanged(); 
            } 
        }

        private DateTime? birthDate;
        public DateTime? BirthDate 
        { get => birthDate; 
            set { birthDate = value; 
                OnPropertyChanged(); 
            } 
        }

        // ---- Ctors ----

        //Add mode
        public CustomerDialogViewModel() { }


        //Edit mode: prefill from existing.
        public CustomerDialogViewModel(Customer existing)
        {
            if (existing == null) return;

            Title = $"Edit Customer (Id={existing.Id})";
            Id = existing.Id;
            FirstName = existing.FirstName;
            LastName = existing.LastName;
            Street = existing.Street;
            No = existing.No;
            PostalCode = existing.PostalCode;
            City = existing.City;
            Email = existing.Email;
            Phone = existing.PhoneNumber;
            BirthDate = existing.BirthDate;
        }

        // ---- Build + Validate ----

      
        /// Validate inputs and build a Customer model.
  
        public bool TryBuildCustomer(out Customer result, out string error, out string focusFieldName)
        {
            result = null;
            error = null;
            focusFieldName = null;

            string firstName = (FirstName ?? "").Trim();
            string lastName = (LastName ?? "").Trim();
            string street = (Street ?? "").Trim();
            string no = (No ?? "").Trim();
            string postalCode = (PostalCode ?? "").Trim();
            string city = (City ?? "").Trim();
            string email = (Email ?? "").Trim();
            string phone = (Phone ?? "").Trim();
            DateTime? bdate = BirthDate;

            if (!ValidationHelper.IsValidFirstName(firstName)) 
            { error = "Invalid first name."; 
                focusFieldName = "TxtFirstName"; 
                return false; }

            if (!ValidationHelper.IsValidLastName(lastName)) 
            { error = "Invalid last name."; 
                focusFieldName = "TxtLastName"; 
                return false; }

            if (!ValidationHelper.IsValidStreet(street)) 
            { error = "Invalid street."; 
                focusFieldName = "TxtStreet"; 
                return false; }

            if (!ValidationHelper.IsNumeric(no)) 
            { error = "No must be a number."; 
                focusFieldName = "TxtNo"; 
                return false; }

            if (!ValidationHelper.IsNumeric(postalCode)) 
            { error = "Postal code must be a number."; 
                focusFieldName = "TxtPostalCode";
                return false; }

            if (!ValidationHelper.IsValidCity(city)) 
            { error = "Invalid city.";
                focusFieldName = "TxtCity"; 
                return false; }

            if (!ValidationHelper.IsValidEmail(email)) 
            { error = "Invalid email format.";
                focusFieldName = "TxtEmail"; 
                return false; }

            
                if (!ValidationHelper.IsValidPhoneNumber(phone)) 
            { error = "Invalid phone number.";
                focusFieldName = "TxtPhone";
                return false; }

            if (bdate == null) { error = "Please select birth date."; 
                focusFieldName = "DpBirthDate"; 
                return false; }

            if (!ValidationHelper.IsValidBirthDate(bdate.Value)) 
            { error = "Invalid birth date."; 
                focusFieldName = "DpBirthDate"; 
                return false; }


            // Build model (date-only)
            result = new Customer
            {
                Id = Id,
                FirstName = firstName,
                LastName = lastName,
                Street = street,
                No = no,
                PostalCode = postalCode,
                City = city,
                Email = email,
                PhoneNumber = phone,
                BirthDate = bdate.Value.Date
            };
            return true;
        }

        // ---- INotifyPropertyChanged ----
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}
