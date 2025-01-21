using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectBank.FileMeneger
{
    internal static class JsonMethods
    {
        public static void JsonSerialize<T>(this T data, string path)
        {
            string serialized = JsonSerializer.Serialize(data, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(path, serialized);
        }

        public static T JsonDeserialize<T> (this T data, string path)
        {
            string asd = File.ReadAllText(path);
            T d = JsonSerializer.Deserialize<T>(asd);
            return d;
        }
    }
}
