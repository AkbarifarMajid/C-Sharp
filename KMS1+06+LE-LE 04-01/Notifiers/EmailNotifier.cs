using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMS1_06_LE_LE_04_01.Events;

namespace KMS1_06_LE_LE_04_01.Notifiers
{
    internal class EmailNotifier : IEventNotifier
    {

        // This method is called when a participant is successfully added to an event.
        public void Notify(object sender, Participant participant)
        {
 
            Console.WriteLine($"Email sent to {participant.Name} at {participant.Email}");
            Console.ReadLine();
        }
    }
}
