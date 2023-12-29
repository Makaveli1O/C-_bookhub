using BookHubWebAPI.Middleware;
using BookHubWebAPI.Swagger;
using BusinessLayer.Facades.Address;
using BusinessLayer.Facades.Author;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.Order;
using BusinessLayer.Facades.BookReview;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.Facades.WishList;
using BusinessLayer.Facades.User;
using BusinessLayer.Mappers;
using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using BusinessLayer.Services.Book;
using BusinessLayer.Services.InventoryItem;
using BusinessLayer.Services.Order;
using BusinessLayer.Services.BookReview;
using BusinessLayer.Services.Publisher;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.UnitOfWork;
using Microsoft.OpenApi.Models;
using BusinessLayer.Facades.BookStore;
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
builder.Services.AddScoped<IInventoryItemFacade, InventoryItemFacade>();

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

builder.Services.AddScoped<IGenericService<BookStore, long>, GenericService<BookStore, long>>();
builder.Services.AddScoped<IBookStoreFacade, BookStoreFacade>();

builder.Services.AddAutoMapper(typeof(AddressProfile));
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(BookReviewProfile));
builder.Services.AddAutoMapper(typeof(BookStoreProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(WishListItemProfile));
builder.Services.AddAutoMapper(typeof(WishListProfile));
builder.Services.AddAutoMapper(typeof(AuthorBookAssociationProfile));


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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseMiddleware<XmlResponseConverterMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
