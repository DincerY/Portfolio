using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfolio.API.Extensions;
using Portfolio.API.Filters;
using Portfolio.Application;
using Portfolio.CrossCuttingConcerns;
using Portfolio.CrossCuttingConcerns.Middleware;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.Contexts;
using Prometheus;
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

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(conf =>
{
    conf.AddPolicy("test1", pol => pol.RequireRole("Admin"));
    conf.AddPolicy("test2", pol => pol.RequireClaim("Admin"));
    conf.AddPolicy("test3", pol => pol.RequireUserName("Admin"));
});

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
});

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration,Log.Logger);

builder.Services.AddHealthChecks();

var app = builder.Build();

//Extension method
app.UseCustomHealthChech();


app.UseHttpMetrics();
app.MapMetrics();


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

app.UseMiddleware<TestMiddleware>();
/*app.UseWhen(c =>
{
    var endpoint = c.GetEndpoint();
    return endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() != null;
}, appBuilder =>
{
    appBuilder.UseMiddleware<CustomAuthenticationMiddleware>();
});

app.UseWhen(c =>
{
    var endpoint = c.GetEndpoint();
    return endpoint?.Metadata.GetMetadata<AuthorizeAttribute>() != null;
}, appBuilder =>
{
    appBuilder.UseMiddleware<CustomAuthorizationMiddleware>();
});*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
