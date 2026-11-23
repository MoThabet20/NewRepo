using BL;
using DAL;
using Domains;
using System.Threading.Tasks;

namespace StoreApp
{
    public class CustomerMenu : BaseMenu
    {
        public override string MenuTitle => "Customer Management";
        Business<Customer> _CustomerBusiness = new Business<Customer>(StorageType.JsonFile);
        public CustomerMenu()
        {

        }
        public override void DisplayOptions()
        {
            Console.WriteLine("[1] View All Customers\r\n" +
            "[2] Search Customer by ID / Phone\r\n" +
            "[3] Add New Customer\r\n" +
            "[4] Update Customer Info\r\n" +
            "[5] Delete Customer\n" +
            "[0] Back " );

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
                        await AddCustomer();
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
                if (_CustomerBusiness.GetAll().Result != null)
                {
                    var customers = await _CustomerBusiness.GetAll();

                    WelcomMessage("ALL CUSTOMERS LIST");
                    Console.WriteLine($"{"ID",-6} | {"Full Name",-22} | {"Phone Number",-16} | {"City",-15}");
                    Console.WriteLine("-------------------------------------------------------------------"); foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.Id,-6} | {customer.Name,-22} | {customer.PhoneNumber,-16} | {customer.Address,-15}");
                    }
                    Console.WriteLine("===============================================================");
                    Console.WriteLine($"Total Records: {customers.Count}");
                    ConsoleInput.Pause();
                }
                else
                {
                    WelcomMessage("ALL CUSTOMERS LIST");
                    Console.WriteLine("[!] INFO: No customers found in the system yet.");
                    Console.WriteLine("===============================================================");
                }

            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine("Business Layer Exception: ");
                ConsoleInput.Pause();
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
                if (customer != null||customer.Id!=0)
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

        async Task AddCustomer()
        {
            try
            {
                Console.Clear();
                Customer oCustomer = new Customer();
                PrintTittle("Add New Customer");
                oCustomer.Id = ConsoleInput.ReadInt("Enter Id: ", 1, int.MaxValue);
                oCustomer.Name = ConsoleInput.ReadString("Enter Full Name: ");
                oCustomer.PhoneNumber = ConsoleInput.ReadString("Enter Phone Number: ");
                oCustomer.Email = ConsoleInput.ReadString("Enter Email: ");
                oCustomer.Address = ConsoleInput.ReadString("Enter Address: ");

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
                        Id =id,
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
