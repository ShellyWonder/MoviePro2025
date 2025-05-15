using System.Text.Json.Serialization;

namespace MoviePro2025.Models.MovieModels
{
    public class SpokenLanguages
    {
        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

}
