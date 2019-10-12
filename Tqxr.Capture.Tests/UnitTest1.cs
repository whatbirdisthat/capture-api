using FluentAssertions;
using Tqxr.Capture.Lib;
using Tqxr.Toy.API;
using Xunit;

namespace Tqxr.Capture.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var startupName = typeof(Startup).Assembly.FullName;
            var class1 = CaptureApi.ControlledApi(startupName);
            class1.Should().NotBeNull();
            Startup.TheVariable.Should().NotBeNullOrEmpty();
        }
    }
}
