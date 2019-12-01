using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ExampleWebApp.IntegrationTests
{
    public class BusinessApiTests
    {
        private readonly HttpClient _httpClient;

        public BusinessApiTests()
        {
            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            var businessApiUrl = configuration["BUSINESS_API_URL"] ?? "http://localhost:5000/api/business";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(businessApiUrl)
            };
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
    }
}