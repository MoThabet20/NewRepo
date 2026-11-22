using BL;
using DAL;
using Domains;

namespace StoreApp
{
    public class OrderMenu : BaseMenu
    {
        public override string MenuTitle => "Order Management";
        Business<Order> _OrderBusiness = new Business<Order>(StorageType.JsonFile);
        Business<Customer> _CustomerBusiness = new Business<Customer>(StorageType.JsonFile);
        Business<Item> _ItemBusiness = new Business<Item>(StorageType.JsonFile);

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
                    await AddOrder();
                    break;
                case 2:
                    await ViewAll();
                    break;
                case 3:
                    await SearchByID();
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
                Console.Clear();
                var orders = await _OrderBusiness.GetAll();
                WelcomMessage("ALL ORDERS LIST");
                Console.WriteLine("ID  |Item Name     |Order Date");
                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id}  |{order.ItemName}     | {order.OrderDate}");
                }
                Console.WriteLine("===============================================================");
                Console.WriteLine($"Total Records: {orders.Count}");
                ConsoleInput.Pause();
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
                int id = ConsoleInput.ReadInt("Enter Order ID: ", 1, int.MaxValue);
                var order = await _OrderBusiness.GetById(id);
                Console.WriteLine("=============Order Details==============");
                if (order != null || order.Id != 0)
                {
                    Console.WriteLine($"Order ID: {order.Id}\n" +
                        $"Item Name: {order.ItemName}\n" +
                        $"Order Date: {order.OrderDate}\n" +
                        $"Quantity: {order.Quantity}\n");

                }
                else
                {
                    Console.WriteLine($"[!] INFO: No order found with ID: {id}");
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
                Order oOrder = new Order()
                {
                    Id = ConsoleInput.ReadInt("Enter Order ID: ", 1, int.MaxValue),
                    OrderDate = DateTime.Now,
                };
                PrintTittle("Add New Order");
                await FillCustomerAndItem(oOrder);
                oOrder.Quantity = ConsoleInput.ReadInt("Enter Quantity: ", 1, int.MaxValue);
                await _OrderBusiness.AddItem(oOrder);
                Console.WriteLine($"[✔] SUCCESS: Order '{oOrder.Id}' added successfully! (Assigned ID: {oOrder.Id})");
                Console.WriteLine("--------------------------------------------------\r\n");
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
                PrintTittle("UPDATE ORDER INFORMATION");
                int orderId = ConsoleInput.ReadInt("Enter Order ID to update: ", 1, int.MaxValue);

                var existing = await _OrderBusiness.GetById(orderId);

                if (existing != null || existing.Id != 0)
                {
                    existing.OrderDate = DateTime.Now;
                    await FillCustomerAndItem(existing);

                    await _OrderBusiness.Update(existing);
                    Console.WriteLine($"[✔] SUCCESS: Order (ID: {orderId}) updated successfully!");
                    ConsoleInput.Pause();
                    Console.WriteLine("--------------------------------------------------------");
                    //Console.WriteLine("Press any key to return to Customers Menu...");
                }
                else
                {
                    Console.WriteLine("Order Not Found");
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
                var Id = ConsoleInput.ReadInt("Enter Id For Delete Order:");
                await _OrderBusiness.Delete(Id);
                Console.WriteLine($"[✔] SUCCESS: Order with ID '{Id}' deleted successfully!");
                ConsoleInput.Pause();
            }
            catch (BusinessLayerException ex)
            {
                {
                    Console.WriteLine("Business Layer Exception: ");
                }
            }


        }

        private async Task FillCustomerAndItem(Order order)
        {
            ClearScreen();
            var customers = await _CustomerBusiness.GetAll();
            WelcomMessage("ALL CUSTOMERS LIST");
            foreach (var item in customers)
            {
                Console.WriteLine($"ID: {item.Id} | Name: {item.Name}");
            }
            Console.WriteLine("--------------------------------------------------");

            while (true)
            {
                order.CustomerId = ConsoleInput.ReadInt("Enter Customer ID for the order: ", 1, int.MaxValue);
                var customer = customers.FirstOrDefault(c => c.Id == order.CustomerId);
                if (customer != null)
                {
                    order.CustomerName = customer.Name;
                    break;
                }
                Console.WriteLine("[!] Invalid Customer ID. Please choose an ID from the list above.");
            }

            ClearScreen(); // يمسح شاشة العملاء ويرسم شاشة المنتجات
            var items = await _ItemBusiness.GetAll();
            WelcomMessage("ALL ITEMS LIST");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id} | Name: {item.Name} | Price: {item.Price}");
            }
            Console.WriteLine("--------------------------------------------------");

            while (true)
            {
                order.itemId = ConsoleInput.ReadInt("Enter Item ID for the order: ", 1, int.MaxValue);
                var itemSelected = items.FirstOrDefault(i => i.Id == order.itemId);
                if (itemSelected != null)
                {
                    order.ItemName = itemSelected.Name;
                    order.Price = itemSelected.Price;
                    break;
                }
                Console.WriteLine("[!] Invalid Item ID. Please choose an ID from the list above.");
            }

            order.Quantity = ConsoleInput.ReadInt("Enter Quantity: ", 1, int.MaxValue);
        }

        void ClearScreen()
        {
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.SetCursorPosition(0, 0);
        }
    }
}
