using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAL
{
    public class JsonConverter<T>: IConverter<T>
    {
        public string Convert(List<T> items)
        {
            return JsonSerializer.Serialize(items);
        }

        public List<T> ConvertBack(string totalContent)
        {
            return JsonSerializer.Deserialize<List<T>>(totalContent);
        }
    }
}
