using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMS1_06_LE_LE_04_01.Events;

namespace KMS1_06_LE_LE_04_01.Notifiers
{

    internal interface IEventNotifier
    {
        // This interface method is used to notify when a participant has been added to an event.
        void Notify (object sender, Participant participant);

    }
}
