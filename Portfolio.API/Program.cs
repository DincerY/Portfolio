using Microsoft.EntityFrameworkCore;
using Portfolio.API.Extensions;
using Portfolio.API.Filters;
using Portfolio.Application;
using Portfolio.CrossCuttingConcerns;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.Contexts;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();


builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ApiResponseFilter>();
});


builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddHealthChecks();

var app = builder.Build();

//Extension method
app.UseCustomHealthChech();


if (app.Environment.IsDevelopment())
{
    app.UseCrossCuttingMiddleware();

}

//app.UseResponseCaching();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Log middleware
//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
