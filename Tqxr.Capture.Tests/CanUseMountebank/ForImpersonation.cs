using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
                .UseEnvironment("development")
                .UseStartup(typeof(ServerAPI))
                .ConfigureServices(services => { });
            
        }
    }

    public class ServerAPI : IStartup
    {
        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }

        public IServiceProvider ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}