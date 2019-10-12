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
        }
    }
}
