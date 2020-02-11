using Microsoft.Extensions.Configuration;
using Tqxr.Toy.API;

namespace Tqxr.Capture.Lib.OperatingModel
{
    public class StartupDevelopment : Startup
    {
        public static TestConfiguration TestConfiguration { get; set; }

        public StartupDevelopment(IConfiguration configuration) : base(configuration)
        {
            string configurationKey = nameof(TestConfiguration);
            string sectionKey = $"{configurationKey}:{nameof(TestConfiguration.MountebankUrl)}";

            TestConfiguration = new TestConfiguration()
            {
                MountebankUrl = configuration[sectionKey]
            };

        }
    }
}
