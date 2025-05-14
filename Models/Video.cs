﻿using System.Text.Json.Serialization;

namespace MoviePro2025.Models
{
    public class Video
    {
        [JsonPropertyName("iso_639_1")]
        public string Iso6391 { get; set; } = string.Empty;

        [JsonPropertyName("iso_3166_1")]
        public string Iso31661 { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("site")]
        public string Site { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("official")]
        public bool Official { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }

}
