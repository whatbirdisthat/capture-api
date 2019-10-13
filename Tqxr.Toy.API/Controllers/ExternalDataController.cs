using System;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tqxr.Toy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExternalDataController : ControllerBase
    {
        public static string EXTERNAL_DATA_API_ENDPOINT = Environment.GetEnvironmentVariable("TOY_API_EXTERNAL_1") ??
                                                          "/list-of-p1";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ExternalDataController> _logger;
        private readonly HttpClient _externalHttpClient;

        public ExternalDataController(ILogger<ExternalDataController> logger, HttpClient externalHttpClient)
        {
            _externalHttpClient = externalHttpClient;
            _logger = logger;
        }

        [HttpGet]
        public ExternalData Get()
        {
            var externalDataAsString = _externalHttpClient.GetStringAsync(EXTERNAL_DATA_API_ENDPOINT).Result;
            var externalData = JsonSerializer.Deserialize<ExternalData>(externalDataAsString);
            return externalData;
        }
    }
}
