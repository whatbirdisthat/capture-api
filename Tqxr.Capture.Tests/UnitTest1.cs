using FluentAssertions;
using Tqxr.Capture.Lib;
using Xunit;

namespace Tqxr.Capture.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var class1 = new CaptureApi();
            class1.Should().NotBeNull();
        }
    }
}