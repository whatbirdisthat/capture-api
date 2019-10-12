using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Tqxr.Capture.Lib.OperatingModel;
using Xunit;

namespace Tqxr.Capture.Tests.CanUseMountebank
{
    public partial class ForImpersonation
    {
        [Fact]
        public void ToCreateANewImposter()
        {
            IWebHostBuilder forFakeWebHost =
                    WebHost.CreateDefaultBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<ServerApi>()
                    .ConfigureServices(services => { });

            forFakeWebHost.Build().Start();

            //            ServerApi.TestConfiguration.MountebankUrl.Should().Be("http://localhost:2525");
        }

    }
}