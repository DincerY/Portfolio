using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Portfolio.API.Extensions;
using Portfolio.API.Filters;
using Portfolio.Application;
using Portfolio.Application.Validators;
using Portfolio.CrossCuttingConcerns;
using Portfolio.CrossCuttingConcerns.Logging.Serilog;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.Contexts;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

//Log asl�nda serilog yap�s�n�n log'u bu y�zden log olu�turup onu UseSerilog i�erisinde 
//kullanmam�za gerek kalmad�
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

//Bu �ekilde kullanarak uyuglaman�n b�t�n log sistemini serilog olarak g�ncelledik
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
    opt.Filters.Add<ApiResponseFilter>();
});

//Bu �ekilde kullan�ld���nda ise ILogger interface'ini serilog olarak IoC'ye att�k

/*builder.Services.AddSerilog(new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger());*/

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


//buraya yazma sebebimiz hatalar� ve loglar� kullan�lan b�t�n middlewareler i�in 
//ge�erli k�lmak istememiz.
app.UseCrossCuttingMiddleware();

app.UseResponseCaching();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Log middleware
//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
