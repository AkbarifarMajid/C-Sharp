using System;
using System.IO;
using System.Threading.Tasks;

namespace KMS_C_LE_05_01.Services
{
    public class FileLoaderService
    {
        //Reads a file asynchronously and returns its content

        public async Task<string> LoadFileAsync(string filePath)
        {
            try
            {
                // Read file content without blocking the main thread

                string content = await Task.Run(() => File.ReadAllText(filePath));

                return content;

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied to file.");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine("I/O error occurred: " + ioEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }

            return null; 
        }
    }
}
