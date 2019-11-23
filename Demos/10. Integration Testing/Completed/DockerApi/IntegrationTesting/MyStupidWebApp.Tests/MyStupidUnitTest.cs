using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyStupidWebApp.Tests
{
    public class MyStupidUnitTest : IClassFixture<ContainerFixture>, IDisposable
    {
        private readonly ContainerFixture _containerFixture;

        private bool _disposedValue = false;
        private string _startedContainerId;

        public MyStupidUnitTest(ContainerFixture containerFixture)
        {
            _containerFixture = containerFixture;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task MyStupidTestCase1()
        {
            // Arrange
            (var containerId, var httpclient) = await _containerFixture.CreateAndStartContainerForTestCase(nameof(MyStupidTestCase1));
            _startedContainerId = containerId;

            // Act
            var hcResponse = await httpclient.GetAsync("/hc");
            var hcResponseContent = await hcResponse.Content.ReadAsStringAsync();

            // Assert
            hcResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_startedContainerId != null)
                    {
                        _containerFixture.RemoveContainerById(_startedContainerId).Wait();
                    }
                }

                _disposedValue = true;
            }
        }
    }
}