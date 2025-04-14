using Microsoft.AspNetCore.DataProtection;
using Serilog;
using Test.Api.Configuration.Models;
using Test.Api.HostedServices;
using Test.Api.Middleware;
using Test.Api.ParameterBinding.FromItem;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => {
    loggerConfig.WriteTo.Console();
});

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddDataProtection();
// IOptions converter!

builder.Services.AddHostedService<TimedHostedService>();

builder.Services.Configure<SecretsConfig>(builder.Configuration.GetSection("Secrets"));



builder.Services.AddControllers(options =>
{

    options.ModelBinderProviders.Insert(0, new FromItemModelBinderProvider());
});

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseMiddleware<SetCorrelationIdMiddleware>();
app.UseMiddleware<RouteVersionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();


class TestX : IDataProtectionProvider
{
    public IDataProtector CreateProtector(string purpose)
    {
        throw new NotImplementedException();
    }
}
