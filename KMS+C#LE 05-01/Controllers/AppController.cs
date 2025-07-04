using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using KMS_C_LE_05_01.Services;

namespace KMS_C_LE_05_01.Controllers
{
    public class AppController
    {
        //EN: Main workflow for the application

        public async Task RunAsync()
        {
            //Create and configure file picker dialog
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Please choose your File! in AppController",
                Filter = "Textdateien (*.txt)|*.txt|PDF-Dateien (*.pdf)|*.pdf|Word-Dateien (*.docx)|*.docx |Alle Dateien (*.*)|*.*"
            };

            // Controll The File
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string myFilePath = ofd.FileName;

                //File was selected by the user
                Console.WriteLine($"File selected from: {myFilePath}");

                try
                {
                    // Instantiate service classes (Make new Object from Services)
                    var loader = new FileLoaderService();
                    var progress = new ProgressService();


                    //Start file reading task
                    Task<string> readTask = loader.LoadFileAsync(myFilePath);

                    // Show "Wait..." while reading is in progress
                    Task progressTask = progress.ShowProgressUntilDone(readTask);

                    //Wait for both reading and progress tasks to complete
                    await Task.WhenAll(readTask, progressTask);


                    //.....................................................

                    //Read content is now ready
                    string fileContent = readTask.Result;

                    Console.WriteLine($"File read successfully.");
                    Console.WriteLine("Preview (first 10 lines):");

                    var readLines = fileContent.Split('\n');
                    // Show up to 10 lines or fewer if file has less
                    for (int i = 0; i < Math.Min(10, readLines.Length); i++)
                    {
                        Console.WriteLine(readLines[i]);
                    }



                    // Ask user if they want to save the file elsewhere
                    Console.WriteLine("\nDo you want to save this file to another location?");
                    Console.WriteLine("Press [Y] to save or any other key to cancel:");

                    var userInputSave = Console.ReadKey();
                    Console.WriteLine();

                    if (userInputSave.Key == ConsoleKey.Y)
                    {
                        //Save the content using SaveService
                        Console.WriteLine("Opening save file dialog...");
                        var saver = new SaveService();
                        saver.SaveToFile(fileContent);
                    }
                    else
                    {
                        //User decided not to save
                        Console.WriteLine("Saving cancelled by user.");
                    }
                }
                catch (Exception ex)
                {
                    //If an error occurs during read
                    Console.WriteLine(" Error reading file: " + ex.Message);
                }
            }
            else
            {
                //No file selected by the user
                Console.WriteLine("No file selected.");
            }
        }
    }
}

/*
// Display the first 10 lines without using a loop
var preview = string.Join(Environment.NewLine, fileContent.Split('\n').Take(10));
Console.WriteLine(preview);
*/