using Microsoft.AspNetCore.Mvc;

namespace Objectbay.Vacation.WebApi.Controllers.Greetings;

[ApiController]
[Route("[controller]")]
public class GreetingsController(
    ILogger<GreetingsController> logger,
    GreetOptions greetOptions
) : ControllerBase
{

    [HttpGet(Name = "GreetMe")]
    public string GreetMe()
    {
        logger.LogInformation("GreetMe called");
        return $"Hello {greetOptions.Who}, you're using {greetOptions.From} environment";
    }
}