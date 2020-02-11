using System.Net;
using System.Net.Http;
using FluentAssertions;
using Tqxr.Capture.Lib;
using Tqxr.Capture.Lib.OperatingModel;
using Xunit;

namespace Tqxr.Capture.Tests.Tqxr.Capture.Can
{
    public class Use_Mountebank_Impersonation
    {
        [Fact]
        public void ToCreateANewImposter()
        {
            CaptureApi forFakeWebHost =
                CaptureApi.ControlledApi(typeof(StartupDevelopment).Assembly.FullName);

            StartupDevelopment.TestConfiguration.MountebankUrl.Should().Be("http://localhost:2525");

            forFakeWebHost.MountebankClient.DeleteAllImposters();

            var hello = new HttpClient().GetStringAsync("http://localhost:2525/imposters/").Result;

            const string expected = @"{
  ""imposters"": []
}";

            hello.Should().Be(expected);


            var theImposter = forFakeWebHost.MountebankClient.CreateHttpImposter(
                9292, "A unit test", false);
            theImposter.AddStub().ReturnsJson(HttpStatusCode.OK, new {P1 = "property one IMPERSONATED"});

            forFakeWebHost.MountebankClient.Submit(theImposter);

            var hello2 = new HttpClient().GetStringAsync("http://localhost:2525/imposters/").Result;
            const string expected2 = @"{
  ""imposters"": [
    {
      ""protocol"": ""http"",
      ""port"": 9292,
      ""numberOfRequests"": 0,
      ""_links"": {
        ""self"": {
          ""href"": ""http://localhost:2525/imposters/9292""
        },
        ""stubs"": {
          ""href"": ""http://localhost:2525/imposters/9292/stubs""
        }
      }
    }
  ]
}";

            hello2.Should().Be(expected2);

            var impostorHello = new HttpClient().GetStringAsync("http://localhost:9292/").Result;

            impostorHello.Should().Be(@"{
    ""P1"": ""property one IMPERSONATED""
}");
        }
    }
}
