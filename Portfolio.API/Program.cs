using FluentValidation;
using FluentValidation.AspNetCore;
using Portfolio.API.Filters;
using Portfolio.Application;
using Portfolio.Application.Validators;
using Portfolio.CrossCuttingConcerns;
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

var app = builder.Build();


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

app.UseCrossCuttingMiddleware();

app.MapControllers();

app.Run();
