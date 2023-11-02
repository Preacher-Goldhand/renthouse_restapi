using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentHouse.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Dodaj usługi do kontenera.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddIdentity<UserModel, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();;
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddControllersWithViews();

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
    name: "order",
    pattern: "{controller=Order}/{action=CreateOrder}");

app.MapControllerRoute(
    name: "userOrders",
    pattern: "{controller=Order}/{action=MyOrders}");

app.MapRazorPages();

app.Run();
