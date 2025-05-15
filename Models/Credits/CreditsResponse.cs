using System.Text.Json.Serialization;

namespace MoviePro2025.Models.Credits
{
    public class CreditsResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cast")]
        public List<Cast> Cast { get; set; } = new List<Cast>();

        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; } = new List<Crew>();
    }

}




