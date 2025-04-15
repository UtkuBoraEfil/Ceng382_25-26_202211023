using System.Text.Json;

namespace RazorProject.Helpers
{
    public class Utils
    {
        private static Utils? _instance;
        public static Utils Instance => _instance ??= new Utils();

        private Utils() { }

        public string ExportToJson<T>(IEnumerable<T> data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
