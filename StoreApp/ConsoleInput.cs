namespace StoreApp
{
    public static class ConsoleInput
    {
        public static int ReadInt(string lable, int? min = null, int? max = null)
        {
            while(true)
            {
                Console.Write(lable);
                var input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                {
                    if (min != null && value < min)
                        Console.WriteLine($"Value must be greater than or equal to {min}.");
                    if (max != null && value > max)
                        Console.WriteLine($"Value must be less than or equal to {max}.");
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        public static decimal ReadDecimal(string lable, decimal? min = null, decimal? max = null)
        {
            while (true)
            {
                Console.Write(lable);
                var input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal value))
                {
                    if (min != null && value < min)
                        Console.WriteLine($"Value must be greater than or equal to {min}.");
                    if (max != null && value > max)
                        Console.WriteLine($"Value must be less than or equal to {max}.");
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a valid decimal.");
            }
        }

        public static string ReadString(string lable)
        {
            while (true)
            {
                Console.Write(lable);
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    return input;
                
                Console.WriteLine("Invalid input. Please enter a valid string.");
                    
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
