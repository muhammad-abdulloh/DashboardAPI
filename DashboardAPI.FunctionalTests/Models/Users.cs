using System.Text.Json.Serialization;

namespace DashboardAPI.FunctionalTests.Models
{
    public class Users
    {
        [JsonPropertyName("Admin")]
        public Admin Admin { get; set; }

        [JsonPropertyName("User")]
        public Admin User { get; set; }
    }
}
