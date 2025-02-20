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



Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
    opt.Filters.Add<ApiResponseFilter>();
});

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


//buraya yazma sebebimiz hatalarý ve loglarý kullanýlan bütün middlewareler için 
//geçerli kýlmak istememiz.
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
