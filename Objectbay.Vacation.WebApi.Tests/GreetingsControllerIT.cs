using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Objectbay.Vacation.WebApi.Tests;

public class GreetingsControllerIT(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_Greeting()
    {
        // Arrange
        var client = factory.CreateClient();

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