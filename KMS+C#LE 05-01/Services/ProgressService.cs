using System;
using System.Threading.Tasks;

namespace KMS_C_LE_05_01.Services
{
    public class ProgressService
    {
        // Displays a progress message every 500ms until the given task is completed

        public async Task ShowProgressUntilDone(Task monitoredTask)
        {
            while (!monitoredTask.IsCompleted)
            {
                Console.WriteLine("Please Wait...");
                await Task.Delay(2000);
            }
        }
    }
}
