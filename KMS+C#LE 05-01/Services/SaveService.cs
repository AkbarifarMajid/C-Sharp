
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace KMS_C_LE_05_01.Services
{
    public class SaveService
    {
        // Saves the given content to a file chosen by the user
        public void SaveToFile(string content)
        {


            // Create a new STA thread to handle SaveFileDialog
            Thread saveThread = new Thread(() =>
            {
                // Configure and show the SaveFileDialog
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Title = "Choose location to save your file",
                    Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*",
                    FileName = "myTestFile.txt"
                };

                // Show the dialog and proceed if user selects a file
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string savePath = sfd.FileName;

                    try
                    {
                        File.WriteAllText(savePath, content);
                        Console.WriteLine($"File saved successfully to: {savePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error saving file: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Saving was cancelled by the user.");
                }
            });



            // Set the thread to STA (Single Threaded Apartment) mode
            saveThread.SetApartmentState(ApartmentState.STA);

            // Start and wait for the thread to finish
            saveThread.Start();
            saveThread.Join();
        }
    }
}

