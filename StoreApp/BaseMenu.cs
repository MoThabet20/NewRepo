using BL;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public abstract class BaseMenu
    {
        protected BaseMenu()
        {
            //decide which storage type to use for the application
            DAL.StorageType storageType = DAL.StorageType.JsonFile;

            //Business layer instances for each entity type
            var itemBL = new Business<Item>(storageType);
            var customerBL = new Business<Customer>(storageType);
            var orderBL = new Business<Order>(storageType);

            //menusfor each entity type
            
        }
        protected bool _isRunning;
        public abstract string MenuTitle { get; }
        public abstract void DisplayOptions();
        public void WelcomMessage(string message)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine($"                     {message}                                ");
            Console.WriteLine("===============================================================");
        }
        public abstract Task HandleUserInput(int choice);
        public async Task Run()
        {
            _isRunning = true;
            while (_isRunning)
            {
               
                Console.Clear();
                // Add your application logic here
                //display the menu options and handle user input
                WelcomMessage(MenuTitle);
                DisplayOptions();
                int choice = ConsoleInput.ReadInt("Choice: ", 0, 5);
                await HandleUserInput(choice);
            }
        }
        protected void Exit()
            { _isRunning=false; }
    }
}
