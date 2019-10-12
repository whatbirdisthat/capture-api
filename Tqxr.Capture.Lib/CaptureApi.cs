using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Tqxr.Capture.Lib
{
    public class CaptureApi : IStartup
    {
        public static IWebHostBuilder ControlledAPI
        {
            get
            {
                return WebHost.CreateDefaultBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<CaptureApi>()
                    .ConfigureServices(services => { });

            }
        }

        public virtual void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }

        public virtual IServiceProvider ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}