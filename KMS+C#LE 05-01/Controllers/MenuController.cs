using System;
using System.Threading.Tasks;
using KMS_C_LE_05_01.Enums;
using KMS_C_LE_05_01.Controllers;
//using KMS_C_LE_05_01.Enums;

namespace KMS_C_LE_05_01.Controllers
{
    public class MenuController
    {
        public async Task ShowMenuAsync()
        {
            Console.Clear();
            Console.WriteLine(" -------------------------------------------");
            Console.WriteLine(" |           FILE READER MENU              |");
            Console.WriteLine(" |-----------------------------------------|");
            Console.WriteLine($" | {(int)MainMenuOption.SimpleMode}. Simple Mode (without events)         |");
            Console.WriteLine($" | {(int)MainMenuOption.EventMode}. Event Mode (using events/delegates)  |");
            Console.WriteLine($" | {(int)MainMenuOption.Exit}. Exit                                 |");
            Console.WriteLine(" |-----------------------------------------|");
            Console.Write("  Please enter your choice (1, 2 or 3): ");

            string input = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(input, out int selection))
            {
                switch ((MainMenuOption)selection)
                {
                    case MainMenuOption.SimpleMode:
                        var simpleController = new AppController();
                        await simpleController.RunAsync();
                        break;

                    case MainMenuOption.EventMode:
                        var eventController = new EventModeController();
                        await eventController.RunAsync();
                        break;

                    case MainMenuOption.Exit:
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a numeric value.");
            }
        }
    }
}

























/*
using System;
using System.Threading.Tasks;

namespace KMS_C_LE_05_01.Controllers
{
    public class MenuController
    {
        public async Task ShowMenuAsync()
        {
            Console.Clear();
            Console.WriteLine(" -------------------------------------------");
            Console.WriteLine(" |           FILE READER MENU              |");
            Console.WriteLine(" |-----------------------------------------|");
            Console.WriteLine(" |1. Simple Mode (without events)          |");
            Console.WriteLine(" |2. Event Mode (using events/delegates)   |");
            Console.WriteLine(" |-----------------------------------------|");
            Console.Write("  Please enter your choice (1 or 2): ");

            //ConsoleKey is an enum, meaning a collection of constant names like:D1 ,D2 ,A,...
            var userInput = Console.ReadKey();
            Console.WriteLine();

            // If user presses 1  run the standard AppController
            if (userInput.Key == ConsoleKey.D1 || userInput.Key == ConsoleKey.NumPad1)
            {
                var controller = new AppController();
                await controller.RunAsync();
            }

            //If user presses 2  run the event-based EventModeController
            else if (userInput.Key == ConsoleKey.D2 || userInput.Key == ConsoleKey.NumPad2)
            {
                var controller = new EventModeController();
                await controller.RunAsync();
            }
            else
            {
                //Any other key show error and exit
                Console.WriteLine($"Invalid choice {userInput.Key} . Exiting...");
            }
        }
    }
}
 */