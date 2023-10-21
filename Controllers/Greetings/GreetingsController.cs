using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dotnet_greet.Controllers.Greetings;

[ApiController]
[Route("[controller]")]
public class GreetingsController : ControllerBase
{
    private readonly ILogger<GreetingsController> _logger;
    private readonly GreetOptions _greetOptions;

    public GreetingsController(
        ILogger<GreetingsController> logger,
        IOptions<GreetOptions> greetOptions) 
    {
        _logger = logger;
        _greetOptions = greetOptions.Value;
    }

    [HttpGet(Name = "GreetMe")]
    public string GreetMe()
    {
        return $"Hello {_greetOptions.Who}, you're using {_greetOptions.From} environment";
    }
}
