using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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

        public virtual void Configure(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
