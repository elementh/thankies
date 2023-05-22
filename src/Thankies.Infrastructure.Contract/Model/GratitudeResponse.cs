using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Thankies.Infrastructure.Contract.Model
{
    public class GratitudeResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("flavours")]
        public List<FlavouredGratitude> Flavours { get; set; }

        public class FlavouredGratitude
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }
    }
}