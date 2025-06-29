using System;
using KMS1_06_LE_LE_04_01.Managers;
using KMS1_06_LE_LE_04_01.Utils;

namespace KMS1_06_LE_LE_04_01.Views.Handlers
{
    internal class EventsAssigner
    {
        private readonly EventManager eventManager;

        public EventsAssigner(EventManager manager)
        {
            this.eventManager = manager;
        }

        public void AssignParticipantToEvent()
        {
            Console.Clear();
            Console.WriteLine("Assign Participant to Event\n");

            try
            {
                // Get event name
                string eventName = PromptForValidName("Please enter the event name: ");

                // Get participant emain
                string email = PromptForValidEmail("Please enter the participant's email: ");



                // Attempt to assign a participant to the event
                bool success = eventManager.AddParticipant(eventName, email);

                if (success)
                {
                    Console.WriteLine("\nParticipant successfully assigned to the event.");
                }
                else
                {
                    Console.WriteLine("\nFailed to assign participant. Either the event or participant doesn't exist, or assignment already done.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        // Helper method to get a valid name
        private string PromptForValidName(string message)
        {
            while (true)
            {

                Console.Write(message);
                string input = Console.ReadLine()?.Trim();

                if (InputValidator.IsValidName(input))
                    return input;

                Console.WriteLine("Name cannot be empty.");

            }
        }


        // Helper method to get valid email
        private string PromptForValidEmail(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim();

                if (InputValidator.IsValidEmail(input))
                    return input;

                Console.WriteLine("The email entered is invalid.");
            }

           
        }
    }
}
