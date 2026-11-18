using System.Threading.Tasks;

namespace StoreApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            BaseMenu app = new MainMenu();
            await app.Run();
        }
    }
}
