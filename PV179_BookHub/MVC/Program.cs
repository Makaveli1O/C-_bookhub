using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.DependencyInjection;
using Infrastructure.DependencyInjection;
using BusinessLayer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.RegisterDALDependencies(configuration);

builder.Services.RegisterInfrastructureDependencies();

builder.Services.RegisterBLDependencies();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<LocalIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<BookHubDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequiredLength = 6;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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