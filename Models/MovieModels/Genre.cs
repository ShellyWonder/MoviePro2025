using System.Text.Json.Serialization;

namespace MoviePro2025.Models.MovieModels
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

}
