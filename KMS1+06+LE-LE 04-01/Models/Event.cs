using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_06_LE_LE_04_01.Events
{

    internal class Event
    {
        public string Name {  get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        // A private list that stores all participants registered in the system.
        private List<Participant> participants = new List<Participant>();



        // Constructor
        public Event() { }
        public Event(string name, DateTime date, string location)
        {
            Name = name;
            Date = date;
            Location = location;
        }

        public List<Participant> GetParticipants()
        {
            return participants;
        }


        // Adds a participant to this specific event, only if their email is not already registered for this event.
        public bool AddParticipant(Participant participant)
        {
            if (participants.Count(p => p.Email == participant.Email) > 0)
            {
                return false;
            }
            participants.Add(participant);
            return true;
        }



    }
}
