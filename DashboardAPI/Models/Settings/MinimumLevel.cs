using System.Text.Json.Serialization;

namespace DashboardAPI.Models.Settings
{
    public class MinimumLevel
    {
        [JsonPropertyName("Default")]
        public string Default { get; set; }
    }
}
