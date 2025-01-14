using System.Text.Json.Serialization;

namespace Disney_Characters.Models
{
    public class CharacterDto
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Films { get; set; }
        public List<string> TvShows { get; set; }
        public string ImageUrl { get; set; }
        public string SourceUrl { get; set; }
    }
}
