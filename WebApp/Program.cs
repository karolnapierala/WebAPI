using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Entities;
using WebApp.Services;
using NLog.Web;
using WebApp.Middleware;
using Microsoft.AspNetCore.Builder;

;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("Data Source=ACER\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;");
var connectionString = builder.Configuration.GetConnectionString("RestaurandDb");

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddTransient<RestaurantSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSwaggerGen();
builder.WebHost.UseNLog();
builder.Services.AddScoped<RequestTimeMiddleware>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<RestaurantSeeder>();
        service.Seed();
    }
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c=> 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantApi");
});

app.MapControllers();

app.Run();


