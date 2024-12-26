using WebApp.Models;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Olympics;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Pobierz connection string
        var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") 
                               ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

        // Konfiguracja DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Konfiguracja Identity
        builder.Services.AddDefaultIdentity<IdentityUser>(options => 
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddRoles<IdentityRole>() // Dodanie ról
        .AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddDbContext<OlympicsContext>(options =>
        {
            options.UseSqlite(builder.Configuration["OlympicsContext:ConnectionString"]);
        });
        // Usługi aplikacji
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        // Rejestracja IContactService (użyj jednej implementacji)
        builder.Services.AddTransient<IContactService, EFContactService>(); // Domyślnie EFContactService
        builder.Services.AddSingleton<IDateTimeProvider, CurrentDateTimeProvider>();

        // Cache i sesje
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();

        var app = builder.Build();

        // Środowisko deweloperskie
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Middleware
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        // Trasy
        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
