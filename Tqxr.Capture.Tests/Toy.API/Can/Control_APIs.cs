using System;
using System.Linq;
using System.Text.Json;
using FluentAssertions;
using Tqxr.Capture.Lib;
using Tqxr.Capture.Lib.OperatingModel;
using Tqxr.Toy.API;
using Tqxr.Toy.API.Controllers;
using Xunit;

namespace Tqxr.Capture.Tests.Toy.API.Can
{
    public class Control_APIs
    {
        private static string controlledApiStartupName = typeof(Startup).Assembly.FullName;

        [Fact]
        public void To_start_a_controlled_API()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            controlledApi.Should().NotBeNull();
            Startup.TheVariable.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void To_Capture_a_response_from_controlled_API()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            string controlledApiResponse = controlledApi.HttpClient.GetStringAsync("/weatherforecast").Result;
            controlledApiResponse.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void To_marshall_responses_to_objects()
        {
            /*
             * var theJson = [{"date":"2019-10-14T11:12:54.1931082+11:00","temperatureC":41,"temperatureF":105,"summary":"Hot"},{"date":"2019-10-15T11:12:54.1932735+11:00","temperatureC":10,"temperatureF":49,"summary":"Mild"},{"date":"2019-10-16T11:12:54.1932764+11:00","temperatureC":12,"temperatureF":53,"summary":"Balmy"},{"date":"2019-10-17T11:12:54.1932768+11:00","temperatureC":6,"temperatureF":42,"summary":"Bracing"},{"date":"2019-10-18T11:12:54.193277+11:00","temperatureC":53,"temperatureF":127,"summary":"Freezing"}]
             */

            WeatherForecast weatherForecast = new WeatherForecast();
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            string controlledApiResponse = controlledApi.HttpClient.GetStringAsync("/weatherforecast").Result;
            WeatherForecast[] forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(controlledApiResponse);
            forecasts.Length.Should().Be(5);
        }

        [Fact]
        public void To_provide_objects_from_controlled_service()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            var forecasts = controlledApi.ProvideAll<WeatherForecast>("/weatherforecast");
            forecasts.Count().Should().Be(5);
        }

        [Fact]
        public void To_UseMountebankImpostersForFakeExternalServices()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
//            var imposterResponse = new {P1 = "Impersonated Value"};
            ExternalData imposterResponse = new ExternalData()
            {
                P1 = "External Data Response"
            };
            controlledApi.MountebankClient.DeleteAllImposters();
            controlledApi.ControlledHttpClient.BaseAddress = new Uri("http://localhost:9292");
            controlledApi.Impersonate(ExternalDataController.EXTERNAL_DATA_API_ENDPOINT, imposterResponse);

            ExternalData externalData = controlledApi.Provide<ExternalData>("/externaldata");

            externalData.Should().BeEquivalentTo(imposterResponse);
        }

        [Fact]
        public void ToCaptureInteractionsBetweenTesterAndService()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            ExternalData imposterResponse = new ExternalData()
            {
                P1 = "External Data Response"
            };
            controlledApi.MountebankClient.DeleteAllImposters();
            controlledApi.ControlledHttpClient.BaseAddress = new Uri("http://localhost:9292");
            controlledApi.Impersonate(ExternalDataController.EXTERNAL_DATA_API_ENDPOINT, imposterResponse);

            var capturedInteraction = controlledApi.Capture<NullObject, ExternalData>("/externaldata");

            capturedInteraction.Response.Should().BeEquivalentTo(imposterResponse, capturedInteraction);
        }
    }
}
