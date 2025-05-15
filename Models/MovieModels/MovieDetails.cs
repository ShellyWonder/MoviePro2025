using System.Text.Json.Serialization;

namespace MoviePro2025.Models.MovieModels
{

    public class MovieDetails
    {
        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; } = string.Empty;

        [JsonPropertyName("budget")]
        public int Budget { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = [];

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("imdb_id")]
        public string ImdbId { get; set; } = string.Empty;

        [JsonPropertyName("original_country")]
        public string[] OriginalCountry { get; set; } = [];

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; } = string.Empty;

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("popularity")]
        public float Popularity { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = string.Empty;

        [JsonPropertyName("production_companies")]
        public List<ProductionCompanies> ProductionCompanies { get; set; } = [];

        [JsonPropertyName("production_countries")]
        public List<ProductionCountries> ProductionCountries { get; set; } = [];

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        [JsonPropertyName("revenue")]
        public int Revenue { get; set; }

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }
        
        [JsonPropertyName("spoken_languages")]
        public List<SpokenLanguages>SpokenLanguages { get; set; } = [];

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("tagline")]
        public string Tagline { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("video")]
        public bool Video { get; set; }

        [JsonPropertyName("vote_average")]
        public float VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }
    }

}
