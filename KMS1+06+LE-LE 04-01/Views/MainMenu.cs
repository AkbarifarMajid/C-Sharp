using System;
using KMS1_06_LE_LE_04_01.Managers;
using KMS1_06_LE_LE_04_01.Utils;
using KMS1_06_LE_LE_04_01.Views.Handlers;
using KMS1_06_LE_LE_04_01.Notifiers;

namespace KMS1_06_LE_LE_04_01.Views
{
    internal class MainMenu
    {
        /* MainMenu Dependencies These readonly fields represent helper components responsible for handling */

        private readonly EventManager eventManager;// Manages all event and participant data

        private readonly EventCreator eventCreator;// Handles creation of new events via user input
        private readonly ParticipantCreator participantCreator;// Handles creation of new participants
        private readonly EventsAssigner assignmentHandler;// Handles assignment of participants to specific events
        private readonly DisplayHandler displayHandler;// Handles displaying lists of events and participants


        /* Constructor of MainMenu class
        Initializes all helper classes with the shared EventManager instance. */
         
        public MainMenu(EventManager manager)
        {
            eventManager = manager; // Store reference to the central event manager

            eventCreator = new EventCreator(eventManager); // Handles event creation logic
            participantCreator = new ParticipantCreator(eventManager); // Handles participant registration
            assignmentHandler = new EventsAssigner(eventManager); // Handles assigning participants to events
            displayHandler = new DisplayHandler(eventManager); // Handles displaying event and participant info
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
               

                Console.WriteLine("|###########################################|");
                Console.WriteLine("|       Event Management System             |");
                Console.WriteLine("|-------------------------------------------|");
                Console.WriteLine("| 1.   Add New Event                        |");
                Console.WriteLine("| 2.   Add New Participant                  |");
                Console.WriteLine("| 3.   Assign Participant to Event          |");
                Console.WriteLine("| 4.   Show All Events                      |");
                Console.WriteLine("| 5.   Show All Participants                |");
                Console.WriteLine("| 6.   Show Events With Participants        |");
                Console.WriteLine("| 7.   Exit                                 |");
                Console.WriteLine("|___________________________________________|");
                Console.Write(" Enter your choice Option: ");
               

                string userInput = Console.ReadLine();

                if (InputValidator.IsValidMenuOption(userInput, 1, 7, out int selectedOption))
                {
                    MainMenuOption UserChoice = (MainMenuOption)selectedOption;

                    switch (UserChoice)
                    {
                        case MainMenuOption.AddEvent:
                            eventCreator.AddNewEvent();
                            break;
                        case MainMenuOption.AddParticipant:
                            participantCreator.AddNewParticipant();
                            break;
                        case MainMenuOption.AssignParticipant:
                            assignmentHandler.AssignParticipantToEvent();
                            break;
                        case MainMenuOption.ShowAllEvents:
                            displayHandler.ShowAllEvents();
                            break;
                        case MainMenuOption.ShowAllParticipants:
                            displayHandler.ShowAllParticipants();
                            break;
                        case MainMenuOption.ShowEventsWithParticipants:
                            displayHandler.ShowAllEventsAndParticipants();
                            break;
                        case MainMenuOption.Exit:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine($"{selectedOption} is not a valid option. Please try again!");
                    Console.ReadKey();
                }
            }
        }
    }
}
