namespace StoreApp
{
    public class MainMenu : BaseMenu
    {

        public override string MenuTitle { get => "Main Menu"; }

        public override void DisplayOptions()
        {
            Console.WriteLine("[1] Customers Management \n" +
                "[2] Items & Inventory Management\n" +
                "[3] Orders Management\n" +
                "[4] Reports Management\n" +
                "[0] Exit");
            Console.WriteLine("===============================================================");
            Console.WriteLine($">>Please enter your choice (0-4):-");
        }

        public override async Task HandleUserInput(int choice)
        {

            switch (choice)
            {
                case 1:
                    CustomerMenu customerMenu = new CustomerMenu();
                    await customerMenu.Run();
                    break;
                case 2:
                    ItemMenu itemMenu = new ItemMenu();
                    await itemMenu.Run();
                    break;
                case 3:
                    OrderMenu orderMenu = new OrderMenu();
                    await orderMenu.Run();
                    break;
                case 4:
                    ReportsMenu reportsMenu = new ReportsMenu();
                    await reportsMenu.Run();
                    break;
                case 0:
                    Exit();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            //return Task.CompletedTask;

        }
    }
}
