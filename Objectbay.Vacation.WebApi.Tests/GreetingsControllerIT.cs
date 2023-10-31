using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Objectbay.Vacation.WebApi.Tests;

public class GreetingsControllerIT
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GreetingsControllerIT(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_Greeting()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/greetings");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var contentType = response?.Content?.Headers?.ContentType?.ToString();
        string content = await (response?.Content?.ReadAsStringAsync() ?? Task.FromResult(""));

        contentType.Should().StartWith("text/plain");
        content.Should().StartWith("Hello").And.Contain(", you're using");
    }
}
