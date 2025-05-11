using System.Text.Json.Serialization;

namespace MoviePro2025.Models
{
    public class ProductionCompanies
    {
        [JsonPropertyName("production_companies")]
        public int Id { get; set; }

        [JsonPropertyName("logo_path")]
        public string LogoPath { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("origin_country")]
        public string OriginCountry { get; set; } = string.Empty;
    }

}
