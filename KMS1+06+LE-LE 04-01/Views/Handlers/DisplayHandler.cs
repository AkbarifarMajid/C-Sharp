using System;
using System.Collections.Generic;
using KMS1_06_LE_LE_04_01.Events;
using KMS1_06_LE_LE_04_01.Managers;

namespace KMS1_06_LE_LE_04_01.Views.Handlers
{
    internal class DisplayHandler
    {
        private readonly EventManager eventManager;

        public DisplayHandler(EventManager manager)
        {
            this.eventManager = manager;
        }

        // Metode Show all events
        public void ShowAllEvents()
        {
            Console.Clear();
            Console.WriteLine("\n All Events:");

            var events = eventManager.GetAllEvents();
            if (events.Count == 0)
            {
                Console.WriteLine("No events found.");
                return;
            }

            foreach (var ev in events)
            {
                Console.WriteLine($"Name Event: {ev.Name} - Date: {ev.Date:yyyy-MM-dd} - Location:  {ev.Location}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        // Method to display all participants
        public void ShowAllParticipants()
        {
            Console.Clear();
            Console.WriteLine("\n All Participants:");

            var participants = eventManager.GetAllParticipants();
            if (participants.Count == 0)
            {
                Console.WriteLine("No participants found.");
                return;
            }

            foreach (var p in participants)
            {
                Console.WriteLine($"Name Participant: {p.Name} Email: ({p.Email})");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        // Method to display all events and their participants
        public void ShowAllEventsAndParticipants()
        {
            Console.Clear();
            Console.WriteLine("\n Events and Their Participants:");

            var events = eventManager.GetAllEvents();
            if (events.Count == 0)
            {
                Console.WriteLine("No events to display.");
                return;
            }

            foreach (var ev in events)
            {
                Console.WriteLine($"\n Name Event: {ev.Name} - Date:  {ev.Date:yyyy-MM-dd} - Location:  {ev.Location}");

                var participants = ev.GetParticipants();
                if (participants.Count == 0)
                {
                    Console.WriteLine("(No participants registered)");
                }
                else
                {
                    foreach (var p in participants)
                    {
                        Console.WriteLine($" Name participant: {p.Name} -  Email: ({p.Email})");
                    }
                    Console.WriteLine("-------------------------");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



    }
}
