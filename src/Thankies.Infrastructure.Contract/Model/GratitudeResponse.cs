using System.Collections.Generic;

namespace Thankies.Infrastructure.Contract.Model
{
    public class GratitudeResponse
    {
        
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}