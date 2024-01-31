using BookHubWebAPI.Swagger;
using Microsoft.OpenApi.Models;
using DataAccessLayer.DependencyInjection;
using Infrastructure.DependencyInjection;
using BusinessLayer.DependencyInjection;
using BusinessLayer.Middleware;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.RegisterDALDependencies(configuration);

builder.Services.RegisterInfrastructureDependencies();

builder.Services.RegisterBLDependencies();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter the API key as follows: topsecret",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.OperationFilter<FormatQueryParameterOperationFilter>(
         "format",
         "The format of the response",
         "json",
         new List<string> {"json", "xml"},
         false);
});

builder.Services.AddLogging();

builder.Services.AddMemoryCache(
        options => { 
            options.ExpirationScanFrequency = TimeSpan.FromSeconds(5);
        }
    );

var app = builder.Build();

if (Convert.ToBoolean(configuration.GetSection("ApplyMigrations").Value))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        db.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>("API");

app.UseHttpsRedirection();

app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseMiddleware<XmlResponseConverterMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
