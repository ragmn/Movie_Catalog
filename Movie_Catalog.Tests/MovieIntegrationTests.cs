using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Movie_Catalog.Tests
{
    public class MovieIntegrationTests
    {
        private readonly HttpClient _client;

        public MovieIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public void GetMoviesTest()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "http://localhost:63063/api/MovieCatalog");

            // Act
            var response = _client.SendAsync(request).Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
