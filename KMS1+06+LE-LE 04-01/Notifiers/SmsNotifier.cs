using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMS1_06_LE_LE_04_01.Events;

namespace KMS1_06_LE_LE_04_01.Notifiers
{
    internal class SmsNotifier : IEventNotifier
    {
        public void Notify(object sender, Participant participant)
        {
            //Codes for sending email

            Console.WriteLine($"SMS sent to {participant.Name} at {participant.Email}");
            Console.ReadLine();
        }
    }
}
