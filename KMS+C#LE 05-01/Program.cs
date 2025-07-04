using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using KMS_C_LE_05_01.Controllers;

namespace KMS_C_LE_05_01
{
    internal class Program
    {
        //Required to enable dialogs like OpenFileDialog which need STA thread mode.
        [STAThread]
        static void Main(string[] args)
        {
            //Entry point of the application. Since 'Main' can't be async directly,we call an async method (MainAsync) from here.
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var myMenu = new MenuController();

            //Show the menu asynchronously
            await myMenu.ShowMenuAsync();
        }
    }
}
