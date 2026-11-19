namespace StoreApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            BaseMenu app = new MainMenu();
            await app.Run();

        }
    }

}
