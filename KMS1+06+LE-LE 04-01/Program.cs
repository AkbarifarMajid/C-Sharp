using System;
using System.Collections.Generic;
using KMS1_06_LE_LE_04_01.Events;
using KMS1_06_LE_LE_04_01.Managers;
using KMS1_06_LE_LE_04_01.Notifiers;
using KMS1_06_LE_LE_04_01.Views;

namespace KMS1_06_LE_LE_04_01
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Create an instance(Interface) of EmailNotifier which implements IEventNotifier
            var emailNotifier = new EmailNotifier();

            var smsNotifier = new SmsNotifier();


            // Create an EventManager and register the EmailNotifier as a subscriber to the ParticipantAdded event
            var eventManager = new EventManager(new List<IEventNotifier> { emailNotifier, smsNotifier});

           
            //var eventManager = new EventManager(emailNotifier);


   
            // Haupu Menu
            var menu = new MainMenu(eventManager);
            menu.ShowMenu();
        }
    }
}
