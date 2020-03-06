using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Thankies.Infrastructure.Contract.Model
{
    public class GratitudeResponse
    {
        public GratitudeResponse()
        {
            Categories = new List<string>();
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("categories")]
        public IEnumerable<string> Categories { get; set; }
    }
}