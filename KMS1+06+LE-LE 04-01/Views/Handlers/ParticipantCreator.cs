using System;
using KMS1_06_LE_LE_04_01.Events;
using KMS1_06_LE_LE_04_01.Managers;
using KMS1_06_LE_LE_04_01.Utils;

namespace KMS1_06_LE_LE_04_01.Views.Handlers
{
    internal class ParticipantCreator
    {
        private readonly EventManager eventManager;

        public ParticipantCreator(EventManager manager)
        {
            this.eventManager = manager;
        }

        public void AddNewParticipant()
        {
            Console.Clear();
            Console.WriteLine("Add New Participant\n");

            try
            {
                // Get participant name
                string name = PromptForValidName("Enter participant name: ");
              
                // Get participant email
                string email = PromptForValidEmail("Enter participant email: ");
                


                // Add a new participant to the system
                Participant participant = new Participant(name, email);
                eventManager.AddParticipant(participant);

                Console.WriteLine("\nParticipant added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }



        // Helper method to get a valid name
        private string PromptForValidName(string message)
        {
            while (true)
            {

                Console.Write(message);
                string inputName = Console.ReadLine()?.Trim();

                if (InputValidator.IsValidName(inputName))
                    return inputName;
                
                Console.WriteLine("Name cannot be empty.");
  
            }
        }

        
        // Helper method to get valid email
        private string PromptForValidEmail(string message)
        {
            while (true)
            {
                
            Console.Write(message);
            string inputMail = Console.ReadLine()?.Trim();

            if (InputValidator.IsValidEmail(inputMail))
                    return inputMail;
              
            Console.WriteLine("The email entered is invalid.");
          
            }
        }
    }
}
