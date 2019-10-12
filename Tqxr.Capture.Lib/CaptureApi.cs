﻿using System.Net.Http;
using MbDotNet;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Tqxr.Capture.Lib
{
    public class CaptureApi
    {
        private TestServer _systemUnderTest;
        private HttpClient _httpClient;
        private MountebankClient _mountebank;

        public HttpClient HttpClient
        {
            get => _httpClient;
        }

        public CaptureApi(TestServer systemUnderTest)
        {
            this._systemUnderTest = systemUnderTest;
            this._httpClient = systemUnderTest.CreateClient();
            this._mountebank = new MountebankClient();
        }

        public static CaptureApi ControlledApi(string startupClass)
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .UseStartup(startupClass)
                .ConfigureServices(services => { });
            TestServer testServer = new TestServer(webHostBuilder);
            return new CaptureApi(testServer);
        }
    }
}
