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
    public class ReportsMenu : BaseMenu
    {
        public override string MenuTitle => "Reports Menu";
       public static Business<Order> _orderBusiness = new Business<Order>(StorageType.JsonFile);
        OrderReportService _reports = new OrderReportService(_orderBusiness);

        public override void DisplayOptions()
        {
            Console.WriteLine("[1] Total Sales Summary\r\n" +
"[2] Sales By Customer\r\n" +
"[3] Sales By Item\r\n" +
"[4] Top Customer (Top 5)\r\n" +
"[5] Top Item (Top 5)\n" +
"[0] Back ");
        }

        public override async Task HandleUserInput(int choice)
        {
            _isRunning = true;

                switch (choice)
                {
                    case 1:
                        await TotalSalesSummary();
                        break;
                    case 2:
                        await SalesByCustomer();
                        break;
                    case 3:
                        await SalesByItem();
                        break;
                    case 4:
                        await TopCustomer();
                        break;
                    case 5:
                        await TopItem();
                        break;
                    case 0:
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            
        }

        private async Task TopItem()
        {
            var top = await _reports.GetTopSalesByItem(5);
            Console.WriteLine("===========Top Items==============");
            foreach (var item in top)
            {
                Console.WriteLine(item.ItemName);
            }
            Console.WriteLine("========================================================");
            ConsoleInput.Pause();
        }

        private async Task TopCustomer()
        {
            var top = await _reports.GetTopSalesByCustomer(5);
            Console.WriteLine("===========Top Customers==============");
            foreach (var item in top)
            {
                Console.WriteLine(item.CustomerName);
            }
            Console.WriteLine("========================================================");
            ConsoleInput.Pause();
        }

        private async Task SalesByItem()
        {
            try
            {
                Console.Clear();
                var items = await _reports.GetSalesByItem();
                Console.WriteLine($"{"Item ID",-10} | {"Item Nmae",-15} | {"Total Revenu",-10} | {"Num of orders",-15}");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.ItemId,-10} | {item.ItemName,-15} | {item.TotalRevenu,-10} | {item.TotalQuantity,-15}");
                }
                Console.WriteLine("==========================================================================");
                ConsoleInput.Pause();
            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine($"Business Layer Exception: {ex.Message}");
                ConsoleInput.Pause();
            }
        }

        private async Task SalesByCustomer()
        {
            try
            {
                Console.Clear();
                var orders = await _reports.GetSalesByCustomer();
                Console.WriteLine($"{"Customer ID",-10} | {"Customer Nmae",-15} | {"Customer Revenu",-10} | {"Num of orders",-15}");
                foreach (var order in orders)
                {
                    //customerOrder
                    Console.WriteLine($"{order.CustomerId,-10} | {order.CustomerName,-15} | {order.TotalRevenu,-10} | {order.OrderCount,-15}");
                }
                Console.WriteLine("==========================================================================");
                ConsoleInput.Pause();
            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine($"Business Layer Exception: {ex.Message}");
               ConsoleInput.Pause();
            }
        }


        private async Task TotalSalesSummary()
        {
            try
            {
                Console.Clear();
                var summary = await _reports.GetTotalSalesSummary();
                Console.WriteLine($"{"Total Sales",-6} | {"Total Price",-25}");
                Console.WriteLine("-----------------------------------------");
                
                Console.WriteLine($"{summary.OrdersCount,-6}  | {summary.TotalSales,-25}");
                ConsoleInput.Pause();
            }
            catch (BusinessLayerException ex)
            {
                Console.WriteLine("Business Layer Exception: " + ex.Message);
            }
        } 

    }
}
