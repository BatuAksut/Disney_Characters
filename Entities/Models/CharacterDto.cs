using Core.Entities;
using System.Text.Json.Serialization;

namespace Disney_Characters.Models
{
    public class CharacterDto : IEntity
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        public string? Name { get; set; }  // Nullable string

        public List<string>? Films { get; set; }  // Nullable List<string>

        public List<string>? TvShows { get; set; }  // Nullable List<string>

        public string? ImageUrl { get; set; }  // Nullable string

        public string? SourceUrl { get; set; }  // Nullable string
    }
}
