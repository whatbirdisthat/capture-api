using System.Net.Http;
using FluentAssertions;
using Tqxr.Capture.Lib;
using Tqxr.Capture.Lib.OperatingModel;
using Xunit;

namespace Tqxr.Capture.Tests.CanUseMountebank
{
    public partial class ForImpersonation
    {
        [Fact]
        public void ToCreateANewImposter()
        {
            CaptureApi forFakeWebHost =
                CaptureApi.ControlledApi(typeof(StartupDevelopment).Assembly.FullName);

            StartupDevelopment.TestConfiguration.MountebankUrl.Should().Be("http://localhost:2525");

            string hello = new HttpClient().GetStringAsync("http://localhost:2525/imposters/").Result;

            string expected = @"{
  ""imposters"": []
}";

            hello.Should().Be(expected);
        }
    }
}
