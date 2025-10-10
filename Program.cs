using ASP.MVC.Services.BookingServices;
using ASP.MVC.Services.HttpHandler;
using ASP.MVC.Services.MenuServices;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ASP.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient("ASP_Reservations", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7275/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
                .AddHttpMessageHandler<JwtAuthorizationHandler>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = "/Admin/Login";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<JwtAuthorizationHandler>();
            builder.Services.AddScoped<IMenuServices, MenuServices>();
            builder.Services.AddScoped<IBookingServices, BookingServices>();
            builder.Logging.AddConsole();
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
            app.UseHttpMethodOverride();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
