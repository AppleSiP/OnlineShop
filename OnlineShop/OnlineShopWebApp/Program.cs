using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Services;
using OnlineShopWebApp.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("online_Shop");

builder.Host.UseSerilog((hostingContext, configuration) => configuration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ApplicationName", "3D Model OnlineShop"));

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true
    };
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
builder.Services.AddTransient<IItemsRepository, ItemsDbRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteDbRepository>();
builder.Services.AddTransient<ICompareRepository, CompareDbRepository>();
builder.Services.AddSingleton<IRolesRepository, RolesInMemoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see
    // https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseStaticFiles();

app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
}

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();

app.UseAuthorization();

app.Run();