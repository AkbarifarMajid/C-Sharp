using System;
using KMS1_06_LE_LE_04_01.Events;
using KMS1_06_LE_LE_04_01.Managers;
using KMS1_06_LE_LE_04_01.Utils;

namespace KMS1_06_LE_LE_04_01.Views.Handlers
{
    internal class EventCreator
    {
        private readonly EventManager eventManager;

        public EventCreator(EventManager manager)
        {
            this.eventManager = manager;
        }


        public void AddNewEvent()
        {
            Console.Clear();
            Console.WriteLine("You are creating a new event.\n");


                // Get the event name
                string name = PromptForValidName("Please enter the event name: ");
                string location = PromptForValidLocationText("Enter the event location: ");
                DateTime date = PromptForValidDate("Enter the event date Correct type ->  yyyy-MM-dd: ");

 

                // Create and add event
                Event newEvent = new Event
                {
                    Name = name,
                    Location = location,
                    Date = date
                };

                eventManager.AddEvent(newEvent);
                Console.WriteLine("\nEvent added successfully!");
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

                Console.WriteLine("Name cannot be empty !");


            }
        }


        // Helper method to get non-empty Location text
        private string PromptForValidLocationText(string message)
        {
            while (true)
            {

                Console.Write(message);
                string inputText = Console.ReadLine()?.Trim();


                if (InputValidator.IsNonEmptyText(inputText))
                
                    return inputText;

                Console.WriteLine("Input cannot be empty.");

              
            }
        }


        // Helper method to get a valid date
        private DateTime PromptForValidDate(string message)
        {
            while (true)
            {

                Console.Write(message);
                string inputDate = Console.ReadLine();

                //DateTime date;
                if (InputValidator.IsValidDate(inputDate, out DateTime date))
                    return date;
                
                Console.WriteLine("Invalid date format. Please use format: yyyy-MM-dd");
                      
            }
        }
    }
}
