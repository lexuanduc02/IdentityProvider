using IdentityProvider;
using IP.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                        .Build();

var services = builder.Services;

var connectionString = builder.Configuration.GetConnectionString("NpgsqlDatabase");
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("=======Has not define connection string yet!!!=======");
    return;
}

SerilogRegister.Initialize(configuration);

// Add services to the container.
services.AddControllersWithViews();

services
    .AddOptionCollection(configuration)
    .AddServiceCollection()
    .AddAutoMapper(typeof(Program));

services
    .AddDbContext<IdentityProviderContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

services
    .AddIdentity<User, Role>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddEntityFrameworkStores<IdentityProviderContext>()
    .AddDefaultTokenProviders();

services
    .AddRouting(options =>
    {
        options.LowercaseUrls = true;
    });

// Seeding data for IdentityServer database on first run
// IdentityServerRegister.Initialize(services, connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

MigrationManager.InitializeDatabase(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
