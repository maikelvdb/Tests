using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApp.Session";
    // Set a short timeout for easy testing
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = ".MyApp.Auth";
        // You can customize login path, etc.
        options.LoginPath = "/auth/login";

        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = context =>
            {
                // Example of how to validate the user's password
                if (context.Principal!.HasClaim(c => c.Type == ClaimTypes.Name))
                {
                    var username = context.Principal.FindFirstValue(ClaimTypes.Name);
                    if (username == "test")
                    {
                        //context.RejectPrincipal();
                        //await context.HttpContext.SignOutAsync();

                    }

                }
                    return Task.CompletedTask;
            },

            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            },

            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast").RequireAuthorization();

app.MapPost("/login", async (HttpContext ctx, LoginRequest loginData) =>
{
    if (loginData.Email == "test" && loginData.Password == "password")
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, loginData.Email),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Results.Ok(new { message = "Login successful" });
    }

    return Results.Unauthorized();
});

app.MapGet("/signout", async (HttpContext ctx) => { 
    await ctx.SignOutAsync();
}).RequireAuthorization();


app.MapFallbackToFile("/index.html");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
