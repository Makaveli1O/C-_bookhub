using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var envName = builder.Environment.EnvironmentName;

Console.WriteLine($"Env Name: {envName}");

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
    .Build();

var sqliteConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<BookHubDbContext>(options =>
{
    options
       .UseSqlite(sqliteConnectionString)
       .LogTo(a => Console.WriteLine(a), LogLevel.Debug)
       .EnableSensitiveDataLogging(true)
       .UseLazyLoadingProxies();
    ;
});


builder.Services.AddIdentity<LocalIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BookHubDbContext>()
    .AddDefaultTokenProviders();

// Configure Identity options for password policy
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Configure the application cookie settings.
builder.Services.ConfigureApplicationCookie(options =>
{
    // Sets the path for the login page.
    // When a user attempts to access a resource that requires authentication and they are not authenticated,
    // they will be redirected to this path.
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

/* -- This is optional ;)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SeminarDBContext>();
    db.Database.Migrate();
}
*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
