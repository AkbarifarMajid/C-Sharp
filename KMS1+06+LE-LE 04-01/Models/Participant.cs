using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_06_LE_LE_04_01.Events
{
 
    internal class Participant

    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Participant()
        {

        }

        // Constructor to initialize a Participant with a name and email address.
        public Participant(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
