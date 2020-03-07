using Microsoft.Extensions.Configuration;

namespace Thankies.Bot.Api.ViewModel
{
    public class HealthConfigViewModel
    {
        public string Basic { get; set; }
        public string Mocking { get; set; }
        public string Shouting { get; set; }
        public string Leet { get; set; }

        public static HealthConfigViewModel ParseFromConfiguration(IConfiguration configuration)
        {
            return new HealthConfigViewModel
            {
                Basic = configuration["Images:Basic"],
                Mocking = configuration["Images:Mocking"],
                Shouting = configuration["Images:Shouting"],
                Leet = configuration["Images:Leet"]
            };
        }
    }
}