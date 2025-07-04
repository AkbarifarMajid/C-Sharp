using System;
using System.IO;
using System.Threading.Tasks;

namespace KMS_C_LE_05_01.Events
{
    public class FileProcessorEvent
    {
        //Publisher

        //Event that fires when file reading starts
        public event Action StartEvent;

        //Event that fires while reading is in progress
        public event Action ProgressEvent;

        //Event that fires after reading is completed
        public event Action CompletedEvant;


        //Reads file content and triggers events during the process
        public async Task<string> RunAsync(string filePath)
        {
            //Trigger start event
            StartEvent?.Invoke(); 
            await Task.Delay(1000); //Simulate processing time

            ProgressEvent?.Invoke();
            //Read file content asynchronously
            string content = await Task.Run(() => File.ReadAllText(filePath));
            await Task.Delay(2000); //Simulate more delay


            CompletedEvant?.Invoke();  

            return content; //Return the full content 
        }
    }
}
