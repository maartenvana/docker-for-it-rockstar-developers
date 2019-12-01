using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExampleWebApp.IntegrationTests
{
    public class BusinessApiTests
    {
        private readonly HttpClient _httpClient;

        public BusinessApiTests()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().CreateLogger();

            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            var businessApiUrl = configuration["BUSINESS_API_URL"] ?? "http://localhost:5000/api/business";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(businessApiUrl)
            };

            WaitForServiceToBeUp();
        }

        [Fact]
        public async Task GetBusiness_ReturnsValidBusiness()
        {
            // Arrange
            var requestUri = "";

            // Act
            var result = await _httpClient.GetAsync(requestUri);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultContent = await result.Content.ReadAsStringAsync();
            resultContent.Should().Be("integration testing");
        }

        private void WaitForServiceToBeUp()
        {
            while (true)
            {
                try
                {
                    var result = _httpClient.GetAsync("").Result;

                    result.StatusCode.Should().Be(HttpStatusCode.OK);

                    return;
                }
                catch (Exception)
                {
                    Log.Information("Service not up yet, waiting...");

                    Thread.Sleep(500);
                    //nothing
                }
            }
        }
    }
}