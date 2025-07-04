using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using KMS_C_LE_05_01.Events;
using KMS_C_LE_05_01.Services;

namespace KMS_C_LE_05_01.Controllers
{
    public class EventModeController
    {
        //Main method for running the program in event-based mode
  
        public async Task RunAsync()
        {
            // File selection dialog
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Please choose your File! in EventModeController",
                Filter = "Textdateien (*.txt)|*.txt|PDF-Dateien (*.pdf)|*.pdf|Word-Dateien (*.docx)|*.docx |Alle Dateien (*.*)|*.*"

            };


            if (ofd.ShowDialog() != DialogResult.OK)
            {
                Console.WriteLine("No file selected.");
                return;
            }

            string filePath = ofd.FileName;
            Console.WriteLine($"File selected: {filePath}");


            try
            {
                // Create instance of the event-based processor
                var processor = new FileProcessorEvent();

                // My Subscribers
                // Register event handlers (what to do on each event)
                processor.StartEvent += () => Console.WriteLine("Reading started.");
                processor.ProgressEvent += () => Console.WriteLine("Reading in progress...");
                processor.CompletedEvant += () => Console.WriteLine("Reading completed.");

                //------------------------------------------------------------------

                // Start reading the file
                string content = await processor.RunAsync(filePath);


                // Show preview – first 10 lines
                Console.WriteLine();
                Console.WriteLine("Preview (first 10 lines):");
                var lines = content.Split('\n');
                for (int i = 0; i < Math.Min(10, lines.Length); i++)
                {
                    Console.WriteLine(lines[i]);
                }


                //Ask user if they want to save the file elsewhere
                Console.WriteLine("\nDo you want to save this file to another location?");
                Console.WriteLine("Press [Y] to save or any other key to cancel:");
                var input = Console.ReadKey();
                Console.WriteLine();

                if (input.Key == ConsoleKey.Y)
                {
                    // Save using the SaveService class
                    Console.WriteLine("Opening save file dialog...");
                    var saver = new SaveService();
                    saver.SaveToFile(content);
                }
                else
                {
                    Console.WriteLine("Saving cancelled by user.");
                }
            }
            catch (Exception ex)
            {
                //Display any error during the process
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
