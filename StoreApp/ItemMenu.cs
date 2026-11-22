using BL;
using DAL;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class ItemMenu : BaseMenu
    {
        public override string MenuTitle => "Item Management";
        Business<Item> _ItemBusiness = new Business<Item>(StorageType.JsonFile);
        public ItemMenu()
        {

        }
        public override void DisplayOptions()
        {
            Console.WriteLine("[1] View All Items\r\n" +
"[2] Search Item by ID / Phone\r\n" +
"[3] Add New Item\r\n" +
"[4] Update Item Info\r\n" +
"[5] Delete Item\n");

        }

        public override async Task HandleUserInput(int choice)
        {

            switch (choice)
            {
                case 1:
                    await ViewAll();
                    break;
                case 2:
                    await SearchByID();
                    break;
                case 3:
                    await AddItem();
                    break;
                case 4:
                    await Update();
                    break;
                case 5:
                    await Delete();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }


        }

        void PrintTittle(string message)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("-----------------------------------------------------");
        }

        async Task ViewAll()
        {
            try
            {
                if (_ItemBusiness.GetAll().Result != null)
                {
                    var items = await _ItemBusiness.GetAll();

                    WelcomMessage("ALL ITEMS LIST");
                    Console.WriteLine("ID          | Item Name             | Quantity    |Price      ");
                    foreach (var item in items)
                    {
                    Console.WriteLine($"{item.Id}  | {item.Name}           | {item.Quantity}  | {item.Price}");
                    }
                    Console.WriteLine("===============================================================");
                    Console.WriteLine($"Total Records: {items.Count}");
                    Console.WriteLine("Press any key to return to Items Menu...");
                    ConsoleInput.Pause();
                }
                else
                {
                    WelcomMessage("ALL ITEMS LIST");
                    Console.WriteLine("[!] INFO: No items found in the system yet.");
                    Console.WriteLine("===============================================================");
                    ConsoleInput.Pause();

                }

            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine("Business Layer Exception: ");
            }
        }

        async Task SearchByID()
        {
            try
            {
                PrintTittle("Search By ID");
                int id = ConsoleInput.ReadInt("Enter Customer ID: ", 1, int.MaxValue);
                var item = await _ItemBusiness.GetById(id);
                Console.WriteLine("=============Item Details==============");
                if (item != null)
                {
                    Console.WriteLine($"Customer ID: {item.Id}\n" +
                        $"Name: {item.Name}\n" +
                        $"Price: {item.Price}\n" +
                        $"Quantity: {item.Quantity}\n");
                    ConsoleInput.Pause();
                }
                else
                {
                    Console.WriteLine($"[!] INFO: No item found with ID: {id}");
                    ConsoleInput.Pause();
                }
                Console.WriteLine("=============================================");
            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine("Business Layer Exception: ");
            }
        }

        async Task AddItem()
        {
            try
            {
                Console.Clear();
                Item oItem = new Item();
                PrintTittle("Add New Item");
                oItem.Id = ConsoleInput.ReadInt("Enter Id: ", 1, int.MaxValue);
                oItem.Name = ConsoleInput.ReadString("Enter Full Name: ");
                oItem.Price = ConsoleInput.ReadDecimal("Enter Price: ");
                oItem.Quantity = ConsoleInput.ReadInt("Enter Quantity: ");

                await _ItemBusiness.AddItem(oItem);
                Console.WriteLine($"[✔] SUCCESS: Item '{oItem.Name}' added successfully! (Assigned ID: {oItem.Id})");
                Console.WriteLine("--------------------------------------------------");  
                ConsoleInput.Pause();
            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine("Business Layer Exception: ");
            }
        }

        async Task Update()
        {
            try
            {
                PrintTittle("UPDATE CUSTOMER INFORMATION");
                int id = ConsoleInput.ReadInt("Enter item ID to update: ", 1, int.MaxValue);

                var existing = await _ItemBusiness.GetById(id);
                if (existing != null && existing.Id != 0)
                {
                    var Updated = new Item
                    {
                        Id = existing.Id,
                        Name = ConsoleInput.ReadString($"{existing.Name} --> New Name:"),
                        Price = ConsoleInput.ReadDecimal($"{existing.Price} -->New Price: "),
                        Quantity = ConsoleInput.ReadInt($"{existing.Quantity} -->New Quantity: "),
                    };

                    await _ItemBusiness.Update(Updated);
                    Console.WriteLine($"[✔] SUCCESS: Item (ID: {id}) updated successfully!");
                    Console.WriteLine("--------------------------------------------------------");
                    ConsoleInput.Pause();
                }
                else
                {
                    Console.WriteLine("item Not Found");
                    ConsoleInput.Pause();
                    return;
                }
            }
            catch (BusinessLayerException ex)
            {
                {
                    Console.WriteLine("Business Layer Exception: ");
                    ConsoleInput.Pause();

                }
            }
        }

        async Task Delete()
        {
            try
            {
                var Id = ConsoleInput.ReadInt("Enter Id For Delete Item:");
                await _ItemBusiness.Delete(Id);
                Console.WriteLine($"[✔] SUCCESS: item with ID '{Id}' deleted successfully!");
                Console.WriteLine("Press any key to return to items Menu...");
                ConsoleInput.Pause();
            }
            catch (BusinessLayerException ex)
            {
                {
                    Console.WriteLine("Business Layer Exception: ");
                }
            }


        }
    }
}
