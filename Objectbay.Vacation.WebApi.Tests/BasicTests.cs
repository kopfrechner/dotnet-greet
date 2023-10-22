using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Objectbay.Vacation.WebApi.Tests;

public class BasicTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Spring_should_be_cool()
    {
        "spring".Should().Be("cool");
    }

    [Theory]
    [InlineData("/greetings")]
    public async Task Get_Greet(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var contnetType = response?.Content?.Headers?.ContentType?.ToString();
        string content = await (response?.Content?.ReadAsStringAsync() ?? Task.FromResult(""));

        contnetType.Should().StartWith("text/plain");
        content.Should().StartWith("Hello").And.Contain(", you're using");
    }
}