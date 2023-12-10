using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RentHouse.Data;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dodaj usługi do kontenera.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); 

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

var app = builder.Build();

// Seeder danych
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    var seeder = services.GetRequiredService<DataSeeder>();
    seeder.Seed();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Wystąpił błąd podczas nasadzania bazy danych.");
}

// Konfiguracja potoku żądań HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllerRoute(
    name: "machines",
    pattern: "{controller=Machine}/{action=GetMachines}");

app.MapControllerRoute(
    name: "allMachines",
    pattern: "{controller=Machine}/{action=GetAllMachines}");

app.MapControllerRoute(
    name: "machineDetails",
    pattern: "{controller=Machine}/{action=GetMachineDetails}/{id?}");

app.MapControllerRoute(
    name: "userOrders",
    pattern: "{controller=Order}/{action=MyOrders}");

app.MapControllerRoute(
    name: "registerUser",
    pattern: "{controller=User}/{action=Register}");

app.MapControllerRoute(
    name: "loginUser",
    pattern: "{controller=User}/{action=Login}");

app.MapControllerRoute(
    name: "createOrder",
    pattern: "{controller=Order}/{action=CreateOrder}");

app.MapControllerRoute(
    name: "removeOrder",
    pattern: "{controller=Order}/{action=RemoveOrder}");

app.MapControllerRoute(
    name: "orders",
    pattern: "{controller=Order}/{action=MyOrders}");

app.MapControllerRoute(
    name: "order",
    pattern: "{controller=Order}/{action=GetOrderById}");

app.MapControllerRoute(
    name: "loginPage",
    pattern: "{controller=User}/{action=GetLoginPage}");

app.MapRazorPages();

app.Run();
