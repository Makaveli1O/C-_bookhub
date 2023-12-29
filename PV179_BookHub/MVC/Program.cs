using BusinessLayer.Facades.Address;
using BusinessLayer.Facades.Author;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.Facades.User;
using BusinessLayer.Facades.WishList;
using BusinessLayer.Services.Author;
using BusinessLayer.Services;
using BusinessLayer.Services.Book;
using BusinessLayer.Services.Publisher;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models.Publication;
using BusinessLayer.Mappers;
using BusinessLayer.Facades.BookReview;
using BusinessLayer.Facades.Order;
using BusinessLayer.Services.BookReview;
using BusinessLayer.Services.InventoryItem;
using BusinessLayer.Services.Order;
using DataAccessLayer.Models.Purchasing;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.RegisterDALDependencies(configuration);

builder.Services.AddScoped<IUnitOfWork, BookHubUnitOfWork>();

builder.Services.AddScoped<IGenericService<Address, long>, GenericService<Address, long>>();
builder.Services.AddScoped<IAddressFacade, AddressFacade>();

builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorFacade, AuthorFacade>();

builder.Services.AddScoped<IGenericService<Publisher, long>, PublisherService>();
builder.Services.AddScoped<IPublisherFacade, PublisherFacade>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookFacade, BookFacade>();

builder.Services.AddScoped<IGenericService<WishList, long>, GenericService<WishList, long>>();
builder.Services.AddScoped<IGenericService<WishListItem, long>, GenericService<WishListItem, long>>();
builder.Services.AddScoped<IWishListFacade, WishListFacade>();

builder.Services.AddScoped<IGenericService<User, long>, GenericService<User, long>>();
builder.Services.AddScoped<IUserFacade, UserFacade>();

builder.Services.AddScoped<IGenericService<OrderItem, long>, GenericService<OrderItem, long>>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderFacade, OrderFacade>();
builder.Services.AddScoped<IBookReviewService, BookReviewService>();
builder.Services.AddScoped<IBookReviewFacade, BookReviewFacade>();

builder.Services.AddAutoMapper(typeof(AddressProfile));
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(BookReviewProfile));
builder.Services.AddAutoMapper(typeof(BookStoreProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(WishListItemProfile));
builder.Services.AddAutoMapper(typeof(WishListProfile));
builder.Services.AddAutoMapper(typeof(AuthorBookAssociationProfile));

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
