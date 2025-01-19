using System.Text.Json.Serialization;

namespace Disney_Characters.Models
{
    // Character.cs
    using System.Text.Json.Serialization;

    namespace Disney_Characters.Models
    {
        public class Character
        {
            [JsonPropertyName("_id")]
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? ImageUrl { get; set; }
            public string? SourceUrl { get; set; }

            private List<string> _films = new();
            private List<string> _tvShows = new();

            [JsonPropertyName("films")]
            public List<string> Films
            {
                get => _films;
                set => _films = value ?? new List<string>();
            }

            [JsonPropertyName("tvShows")]
            public List<string> TvShows
            {
                get => _tvShows;
                set => _tvShows = value ?? new List<string>();
            }
        }
    }
}