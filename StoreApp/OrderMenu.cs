using BL;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class OrderMenu : BaseMenu
    {
        public override string MenuTitle => "Order Management";
        Business<Order> _OrderBusiness = new Business<Order>();
        Business<Customer> _CustomerBusiness = new Business<Customer>();
        Business<Item> _ItemBusiness = new Business<Item>();

        public override void DisplayOptions()
        {
            Console.WriteLine("[1] Create New Order\r\n" +
                "[2] View All Orders\r\n" +
                "[3] View Order Details (Items & Total)\r\n" +
                "[4] Update Order Status (Pending/Delivered)\r\n" +
                "[5] Cancel / Delete Order\n" +
                "[0] Back to main menu");
        }

        public override async Task HandleUserInput(int choice)
        {
            _isRunning = true;

            switch (choice)
            {
                case 1:
                    await ViewAll();
                    break;
                case 2:
                    await SearchByID();
                    break;
                case 3:
                    await AddOrder();
                    break;
                case 4:
                    await Update();
                    break;
                case 5:
                    await Delete();
                    break;
                case 0:
                    Exit();
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
                if (_OrderBusiness.GetAll().Result != null)
                {
                    var orders = await _OrderBusiness.GetAll();
                    var customers = await _CustomerBusiness.GetAll();
                    var items = await _ItemBusiness.GetAll();

                    WelcomMessage("ALL ORDERS LIST");
                    Console.WriteLine("ID  | Item Name             | Order Date    |City      ");
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"{order.Id}  | {order.ItemName}         | {order.OrderDate}  | ");
                    }
                    Console.WriteLine("===============================================================");
                    Console.WriteLine($"Total Records: {orders.Count}");
                    Console.WriteLine("Press any key to return to Customers Menu...");
                    ConsoleInput.Pause();
                }
                else
                {
                    WelcomMessage("ALL CUSTOMERS LIST");
                    Console.WriteLine("[!] INFO: No customers found in the system yet.");
                    Console.WriteLine("===============================================================");
                    Console.WriteLine("Press any key to return to Customers Menu...");

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
                var customer = await _CustomerBusiness.GetById(id);
                Console.WriteLine("=============Customer Details==============");
                if (customer != null || customer.Id != 0)
                {
                    Console.WriteLine($"Customer ID: {customer.Id}\n" +
                        $"Name: {customer.Name}\n" +
                        $"Phone: {customer.PhoneNumber}\n" +
                        $"Address: {customer.Address}\n");

                }
                else
                {
                    Console.WriteLine($"[!] INFO: No customer found with ID: {id}");
                }
                ConsoleInput.Pause();
                Console.WriteLine("=============================================");
            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine("Business Layer Exception: ");
            }
        }

        async Task AddOrder()
        {
            try
            {
                Console.Clear();
                Order oOrder = new Order();
                Customer oCustomer = new Customer();
                Item oItem = new Item();
                PrintTittle("Add New Order");

                await _CustomerBusiness.AddItem(oCustomer);
                Console.WriteLine($"[✔] SUCCESS: Customer '{oCustomer.Name}' added successfully! (Assigned ID: {oCustomer.Id})");
                Console.WriteLine("--------------------------------------------------\r\n" +
                    "Press any key to return to Customers Menu...");
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
                int id = ConsoleInput.ReadInt("Enter Customer ID to update: ", 1, int.MaxValue);

                var existing = await _CustomerBusiness.GetById(id);

                if (existing != null || existing.Id != 0)
                {

                    var Updated = new Customer
                    {
                        Id = id,
                        Name = ConsoleInput.ReadString($"({existing.Name})New Full Name:"),
                        PhoneNumber = ConsoleInput.ReadString($"({existing.PhoneNumber})New Phone Number: "),
                        Email = ConsoleInput.ReadString($"({existing.Email})New Email: "),
                        Address = ConsoleInput.ReadString($"({existing.Address})New Address: ")
                    };

                    await _CustomerBusiness.Update(Updated);
                    Console.WriteLine($"[✔] SUCCESS: Customer (ID: {id}) updated successfully!");
                    ConsoleInput.Pause();
                    Console.WriteLine("--------------------------------------------------------");
                    //Console.WriteLine("Press any key to return to Customers Menu...");
                }
                else
                {
                    Console.WriteLine("Customer Not Found");
                    ConsoleInput.Pause();
                    return;
                }
            }
            catch (BusinessLayerException ex)
            {
                {
                    Console.WriteLine("Business Layer Exception: ");
                }
            }
        }

        async Task Delete()
        {
            try
            {
                var Id = ConsoleInput.ReadInt("Enter Id For Delete Item:");
                await _CustomerBusiness.Delete(Id);
                Console.WriteLine($"[✔] SUCCESS: Customer with ID '{Id}' deleted successfully!");
                ConsoleInput.Pause();
                Console.WriteLine("Press any key to return to Customers Menu...");
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
