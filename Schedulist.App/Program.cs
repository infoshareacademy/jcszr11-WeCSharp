using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;



namespace Schedulist.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cultureInfo = new System.Globalization.CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = cultureInfo;

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<DBContact>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DBContact>();
            builder.Services.AddControllersWithViews();

            User user = new User() { Id = 4, Name = "Zbigniew", Surname = "Zero", Login = "Log4", Password = "Pass3", AdminPrivilege = true, DepartmentId = 1, PositionId = 1 };
            builder.Services.AddSingleton<User>(user);

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICalendarEventRepository, CalendarEventRepository>();
            builder.Services.AddTransient<ICalendarRepository, CalendarRepository>();
            builder.Services.AddTransient<IWorkModeForUserRepository, WorkModeForUserRepository>();
            builder.Services.AddTransient<IWorkModeRepository, WorkModeRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Calendar/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Calendar}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
