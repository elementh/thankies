using System.Collections.Generic;

namespace Thankies.Bot.Api.ViewModel
{
    public class HealthInfrastructureViewModel
    {
        public string Gratitude { get; }
        public List<string> GratitudeFilters { get; }

        public HealthInfrastructureViewModel(string gratitude, List<string> gratitudeFilters)
        {
            Gratitude = gratitude;
            GratitudeFilters = gratitudeFilters;
        }
    }
}