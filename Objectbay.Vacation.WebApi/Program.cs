using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers().AddJsonOptions(
    o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
);
builder.Services.AddSwaggerGen();


// Configure DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("mypostgres"));
});


// Configure Options
builder.Services.Configure<Objectbay.Vacation.WebApi.Controllers.GreetOptions>(
    builder.Configuration.GetSection(nameof(Objectbay.Vacation.WebApi.Controllers.GreetOptions))
);


// Configure app
var app = builder.Build();
app.ApplyMigrations();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


// Start application
app.Run();


public partial class Program { }

public static class WebApplicationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }
    }
}