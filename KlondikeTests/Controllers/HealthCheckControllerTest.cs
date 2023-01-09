using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace KlondikeTests.Controllers;

public class HealthCheckControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthCheckControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestGetHealthCheck()
    {
        // Arrange
        var expected = "";

        // Act
        var response = await _client.GetAsync("/HealthCheck");
        var actual = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expected, actual);
    }
}
