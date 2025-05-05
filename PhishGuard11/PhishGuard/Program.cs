using Microsoft.EntityFrameworkCore;

namespace PhishGuard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SiteDb>(opts =>
            opts.UseSqlServer(builder.Configuration.GetConnectionString("Site")));

            builder.Services.AddDistributedMemoryCache();  
            builder.Services.AddSession(opts =>             
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(5);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;

            });
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
