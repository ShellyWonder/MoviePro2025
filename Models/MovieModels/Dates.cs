using System.Text.Json.Serialization;

namespace MoviePro2025.Models.MovieModels
{
    public class Dates
    {
        [JsonPropertyName("maximum")]
        public string Maximum { get; set; } = string.Empty;

        [JsonPropertyName("minimum")]
        public string Minimum { get; set; }= string.Empty;
    }

  

}
