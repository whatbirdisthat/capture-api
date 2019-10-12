using System;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tqxr.Toy.API;
using Xunit;

namespace Tqxr.Capture.Tests.CanUseMountebank
{
    public class ForImpersonation
    {
        [Fact]
        public void ToCreateANewImposter()
        {
            IWebHostBuilder forFakeWebHost = WebHost.CreateDefaultBuilder();
            forFakeWebHost
                .UseEnvironment("Development")
                .UseStartup(nameof(ServerAPI))
                .ConfigureServices(services => { });

            ServerAPI.TestConfiguration.MountebankUrl.Should().Be("http://localhost:2525");
        }
        
        
        struct TestConfiguration
        {
            public string MountebankUrl { get; set; }
        }

        class ServerAPI : Startup
        {
            internal static TestConfiguration TestConfiguration { get; set; }

            public ServerAPI(IConfiguration configuration) : base(configuration)
            {
                TestConfiguration = configuration.As<TestConfiguration>();
            }
        }        
    }


}