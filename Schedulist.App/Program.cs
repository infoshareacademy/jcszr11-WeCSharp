using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schedulist.App.Middleware;
using Schedulist.App.Services;
using Schedulist.App.Services.Interfaces;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;
using Serilog;



namespace Schedulist.App
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var cultureInfo = new System.Globalization.CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = cultureInfo;

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((hostingBuilderContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostingBuilderContext.Configuration));

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SchedulistDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
                {
                    //options.Password.RequiredUniqueChars = 0;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequireLowercase = false;
                    //options.Password.RequiredLength = 8;
                    //options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedAccount = true;
                }
                )
                .AddEntityFrameworkStores<SchedulistDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            User user = new() { Id = "2", Name = "Andrzej", Surname = "Andrzejewski", DepartmentId = 3, PositionId = 4 };
            builder.Services.AddSingleton<User>(user);
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICalendarEventRepository, CalendarEventRepository>();
            builder.Services.AddScoped<ICalendarEventService, CalendarEventService>();
            builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
            builder.Services.AddTransient<IWorkModeForUserRepository, WorkModeForUserRepository>();
            builder.Services.AddTransient<IWorkModeRepository, WorkModeRepository>();
            //builder.Services.AddScoped<ErrorHandlingMiddleware>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                //var dbSeed = scope.ServiceProvider.GetService<DBSeed>();
                //await dbSeed.CreateAdmin();
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Calendar/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseMiddleware<ErrorHandlingMiddleware>();
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
