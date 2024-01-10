using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.DependencyInjection;
using Infrastructure.DependencyInjection;
using BusinessLayer.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BusinessLayer.Middleware;

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

app.UseMiddleware<RequestLoggingMiddleware>("MVC");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);



using (var scope = app.Services.CreateScope())
{
    // SampleData.Initialize(scope.ServiceProvider);
    // await CreateRolesAndUsers(scope.ServiceProvider);
}

app.Run();


static async Task CreateRolesAndUsers(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<BookHubDbContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<LocalIdentityUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create roles
    string[] roleNames = { "Admin", "Manager", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
            Console.WriteLine("created role");
        }
    }

    foreach (var usr in context.Users)
    {
        var identityUser = new LocalIdentityUser
        {
            UserName = usr.UserName,
            Email = usr.UserName,
            User = usr,
        };
        await userManager.CreateAsync(identityUser, "password");
        // await userManager.AddToRoleAsync(identityUser, "User");
        Console.WriteLine("created user");

    }


    // Create admin user
    var adminUser = new LocalIdentityUser
    {
        UserName = "admin@admin.com",
        Email = "admin@admin.com",
        User = new User() {UserName = "admin@admin.com" }
    };

    var user = await userManager.FindByEmailAsync(adminUser.Email);
    if (user == null)
    {
        await userManager.CreateAsync(adminUser, "admin");
        Console.WriteLine("created");
        // await userManager.AddToRoleAsync(adminUser, "Admin");
    }
    await context.SaveChangesAsync();
}

public class SampleData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetService<BookHubDbContext>();

        string[] roles = new string[] { "User", "Admin", "Manager" };

        foreach (string role in roles)
        {
            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == role))
            {
                roleStore.CreateAsync(new IdentityRole(role));
            }
        }


        var user = new LocalIdentityUser
        {
            Email = "xxxx@example.com",
            NormalizedEmail = "XXXX@EXAMPLE.COM",
            UserName = "Owner",
            NormalizedUserName = "OWNER",
            PhoneNumber = "+111111111111",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };


        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<LocalIdentityUser>();
            var hashed = password.HashPassword(user, "secret");
            user.PasswordHash = hashed;

            var userStore = new UserStore<LocalIdentityUser>(context);
            var result = userStore.CreateAsync(user);

        }

        AssignRoles(serviceProvider, user.Email, roles);

        context.SaveChangesAsync();
    }

    public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
    {
        UserManager<LocalIdentityUser> _userManager = services.GetService<UserManager<LocalIdentityUser>>();
        LocalIdentityUser user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.AddToRolesAsync(user, roles);

        return result;
    }

}