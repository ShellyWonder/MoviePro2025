using System.Text.Json.Serialization;

namespace MoviePro2025.Models
{
    public class ProductionCountries
    {
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

}
