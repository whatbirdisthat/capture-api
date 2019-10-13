using System.Text.Json;
using FluentAssertions;
using Tqxr.Capture.Lib;
using Tqxr.Toy.API;
using Xunit;

namespace Tqxr.Capture.Tests.Toy.API.Can
{
    public class Control_APIs
    {
        private static string controlledApiStartupName = typeof(Startup).Assembly.FullName;

        [Fact]
        public void Start_A_Controlled_API()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            controlledApi.Should().NotBeNull();
            Startup.TheVariable.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Capture_a_response_from_Controlled_API()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            string controlledApiResponse = controlledApi.HttpClient.GetStringAsync("/weatherforecast").Result;
            controlledApiResponse.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Marshall_responses_to_objects()
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
        public void Provide_Objects_From_Controlled_Service()
        {
            var controlledApi = CaptureApi.ControlledApi(controlledApiStartupName);
            WeatherForecast[] forecasts = controlledApi.ProvideAll<WeatherForecast>("/weatherforecast");
            forecasts.Length.Should().Be(5);
        }
    }
}
