using System.Text.Json.Serialization;

namespace MoviePro2025.Models
{
    public class MovieVideosResponse

    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("results")]
        public List<Video> Results{ get; set; } = [];
    }

}
