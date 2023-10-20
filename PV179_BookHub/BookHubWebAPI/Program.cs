using DataAccessLayer.Data;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<BookHubDbContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var dbPath = Path.Join(Environment.GetFolderPath(folder), "BookHub.db");

    options
        .UseSqlite(
            $"Data Source={dbPath}",
            x => x.MigrationsAssembly("DAL.SQLite.Migrations")
            )
        .UseLazyLoadingProxies();
});
builder.Services.AddScoped<IUnitOfWork, BookHubUnitOfWork>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
