using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;
using WebApp;
using WebApp.Authorization;
using WebApp.Entities;
using WebApp.Middleware;
using WebApp.Models;
using WebApp.Models.Validators;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("Data Source=ACER\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;");
var connectionString = builder.Configuration.GetConnectionString("RestaurandDb");

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "Polish"));
    option.AddPolicy("Atleast20", builder => builder.AddRequirements(new CreatedRestaurantsRequierement(20)));
    option.AddPolicy("CreatedAtleast2Restaurants", builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequierement(2)));
});

builder.Services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequierementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<RestaurantDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddTransient<RestaurantSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.WebHost.UseNLog();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

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
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c=> 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantApi");
});
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


