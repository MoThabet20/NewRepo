using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace Utilities
{
    public static class FileHelper
    {
        //support UTF-8
        public static Encoding Utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        //ask for directory path
        private static string Normalize(string path)
            => Path.GetFullPath(path ?? throw new ArgumentException());

        private static void EnsureDirectory(string fullFilepath)
        {
            var dir = Path.GetDirectoryName(fullFilepath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        //create
        public static async Task Create(string path, string content)
        {
            //ask suer to enter directory path
            var full = Normalize(path);
            //ensure if directory exists

                EnsureDirectory(full);
     

                await File.WriteAllTextAsync(full, content ?? string.Empty, Utf8NoBom);
        }

        //Read
        public static async Task<string> ReadAll(string path)
        {
            //ask suer to enter directory path
            var full = Normalize(path);
            if (!File.Exists(full))
            {
                EnsureDirectory(full);
               await File.WriteAllTextAsync(full, "[]", Utf8NoBom);
                return "[]";
            }
            var content = await File.ReadAllTextAsync(full, Utf8NoBom);
            return string.IsNullOrWhiteSpace(content) ? "[]" : content;


        }

        public static async Task<string[]> ReadAllLines(string path)
        {
            //ask suer to enter directory path
            var full = Normalize(path);
            if (!File.Exists(full))
            {
                EnsureDirectory(full);
                await File.WriteAllTextAsync(full, string.Empty, Utf8NoBom);
                return new string[1];
            }
            return await File.ReadAllLinesAsync(full, Utf8NoBom);
        }

        public static async Task Append(string path, string content)
        {
            //ask suer to enter directory path
            var full = Normalize(path);
            EnsureDirectory(full);
            await File.AppendAllTextAsync(full, "\n" + content, Utf8NoBom);
            Console.WriteLine("File Appended....");

        }


        public static async Task Update(string path, string content)
        {
            //ask suer to enter directory path
            var full = Normalize(path);
            EnsureDirectory(full);
            await File.WriteAllTextAsync(full , content, Utf8NoBom);
            Console.WriteLine("File Appended....");

        }

        //TryCreate
        public static async Task<bool> TryCreate(string path, string content)
        {
            try { await Create(path, content); return true; }
            catch { return false; }
        }

        //Tryread
        public static async Task<bool> TryReadAll(string path, string content)
        {
            try
            {
                content = await ReadAll(path); return true;
            }
            catch
            {
                content = null; return false;
            }
        }

        //Append
        //delete
        public static async Task<bool> Delete(string path)
        {
            //ask suer to enter directory path
            var full = Normalize(path);
            EnsureDirectory(full);
            if (!File.Exists(full)) return false;
            File.Delete(full);
            return true;
        }

    }
}
