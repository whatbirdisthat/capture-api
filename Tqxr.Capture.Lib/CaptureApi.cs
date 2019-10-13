using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using MbDotNet;
using MbDotNet.Enums;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Tqxr.Capture.Lib.OperatingModel;

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

        public MountebankClient MountebankClient
        {
            get => _mountebank;
        }

        public void Impersonate<T>(string endpoint, T response)
        {
            var theImposter = MountebankClient.CreateHttpImposter(
                9292, "A unit test", false);

            var theStub = theImposter.AddStub();
            theStub
                .OnPathAndMethodEqual(endpoint, Method.Get)
                .ReturnsJson(HttpStatusCode.OK, response);

            MountebankClient.Submit(theImposter);
        }

        public HttpClient ControlledHttpClient { get; set; }

        public CaptureApi(IWebHostBuilder webHostBuilder)
        {
            this._mountebank = new MountebankClient();
            this.ControlledHttpClient = new HttpClient();
            webHostBuilder.ConfigureServices(services => { services.AddSingleton(ControlledHttpClient); });
            this._systemUnderTest = new TestServer(webHostBuilder);
            this._httpClient = this._systemUnderTest.CreateClient();
        }

        public static CaptureApi ControlledApi(string startupClass)
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .UseStartup(startupClass)
                .ConfigureServices(services => { });

            return new CaptureApi(webHostBuilder);
        }

        public ServerInteraction<TRequest, TResponse> Capture<TRequest, TResponse>(string endpointUrl)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, endpointUrl))
            {
                using (HttpResponseMessage responseMessage = _httpClient.GetAsync(endpointUrl).Result)
                {
                    ServerInteraction<TRequest, TResponse> serverInteraction = new ServerInteraction<TRequest, TResponse>()
                    {
                        RequestAsString = $"{requestMessage}",
                        ResponseAsString = $"{responseMessage}",
                        Request = JsonSerializer.Deserialize<TRequest>(
                            requestMessage.Content?.ReadAsStringAsync().Result ?? "null"),
                        Response = JsonSerializer.Deserialize<TResponse>(
                            responseMessage.Content?.ReadAsStringAsync().Result ?? "null")
                    };
                    return serverInteraction;
                }
            }
        }

        public T Provide<T>(string endpointPath)
        {
            string controlledApiResponse = this.HttpClient.GetStringAsync(endpointPath).Result;
            var theT = JsonSerializer.Deserialize<T>(controlledApiResponse);
            return theT;
        }

        public IEnumerable<T> ProvideAll<T>(string endpoint)
        {
//            this.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string controlledApiResponse = this.HttpClient.GetStringAsync(endpoint).Result;
            return JsonSerializer.Deserialize<T[]>(controlledApiResponse);
        }
    }
}
