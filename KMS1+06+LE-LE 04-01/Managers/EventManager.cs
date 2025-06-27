using System;
using System.Collections.Generic;
using System.Linq;
using KMS1_06_LE_LE_04_01.Events;
using KMS1_06_LE_LE_04_01.Notifiers;


namespace KMS1_06_LE_LE_04_01.Managers
{

    internal class EventManager
    {
        // A list to store all registered participantsList
        private List<Participant> participantsList = new List<Participant>();

        // A list to store all created eventsList in the system
        private List<Event> eventsList = new List<Event>();



        // Delegate definition used for handling participant added eventsList
        public delegate void ParticipantAddedHandler(object sender, Participant participant);

        // Event that is triggered when a new participant is added
        public event ParticipantAddedHandler EventParticipantAdded;




        // Constructor : Subscribes the passed IEventNotifier to the ParticipantAdded event
        public EventManager(IEventNotifier notifier)
        {
            EventParticipantAdded += notifier.Notify; // Adds the Notify method as a subscriber to the event
        }


        // Adds a new event to the list if there is no other event with the same name
        public void AddEvent(Event myEvent)
        {
            if(eventsList.Count(e => e.Name == myEvent.Name) == 0)
                eventsList.Add(myEvent);
        }


        // Adds a new participant to the list if no participant with the same name already exists
        public void AddParticipant(Participant myParticipant)
        {
            if (participantsList.Count(e => e.Email == myParticipant.Email) == 0)
                participantsList.Add(myParticipant);
        }


        // Returns the list of all created events
        public List<Event> GetAllEvents()
        {
            return eventsList;
        }


        // Returns the list of all registered participants
        public List<Participant> GetAllParticipants()
        {
            return participantsList; 
        }



        /* metode add participant to a specific event by name(Puplicher)
         Returns false if the event or participant is not found.*/

        public bool AddEventParticipant(string eventName ,string participantEmail)
        {
            var currentEvent = eventsList.FirstOrDefault(e => e.Name == eventName);
            if (currentEvent == null)
            {
                return false;
            }

            var currentParticipant = participantsList.FirstOrDefault(p => p.Email == participantEmail);
            if (currentParticipant == null)
            {
                return false;
            }


            // Ensures that the event is only triggered once per participant per event.
            var result = currentEvent.AddParticipant(currentParticipant);

            if (result)
            {
                EventParticipantAdded?.Invoke(this, currentParticipant); //If successful adds the participant to the event and triggers the ParticipantAdded event.

            }

            return true;
        }
    }
}
