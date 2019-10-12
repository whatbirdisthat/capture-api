using Microsoft.Extensions.Configuration;
using Tqxr.Toy.API;

namespace Tqxr.Capture.Lib.OperatingModel
{
    public class ServerApi : Startup
    {
        public static TestConfiguration TestConfiguration { get; set; }

        public ServerApi(IConfiguration configuration) : base(configuration)
        {
            var theSection = configuration.GetSection(nameof(TestConfiguration));
            var stringTestConfiguration = theSection.Value;
        }
    }
}