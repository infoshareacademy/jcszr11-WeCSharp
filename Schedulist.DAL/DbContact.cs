using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schedulist.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class DBContact : IdentityDbContext<IdentityUser>
    {
        public DBContact(DbContextOptions<DBContact> options) : base(options) { }


        public DbSet<WorkModesToUser> WorkModesToUsers { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkMode> WorkModes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<WorkMode>().HasData(
                new WorkMode() { Id = 1, Name = "Office" },
                new WorkMode() { Id = 2, Name = "HomeOffice" },
                new WorkMode() { Id = 3, Name = "SickLeave" },
                new WorkMode() { Id = 4, Name = "Delegation" },
                new WorkMode() { Id = 5, Name = "Holiday" },
                new WorkMode() { Id = 6, Name = "Others" }
            );
            base.OnModelCreating(builder);
            builder.Entity<CalendarEvent>().HasData(
                new CalendarEvent() { CalendarEventId = 1, CalendarEventName = "Maintenance Work", CalendarEventDescription = "Ongoing maintenance tasks in the office", CalendarEventDate = new DateOnly(2024, 01, 10), CalendarEventStartTime = new TimeOnly(08, 30), CalendarEventEndTime = new TimeOnly(09, 30) },
                new CalendarEvent() { CalendarEventId = 2, CalendarEventName = "Office Cleaning", CalendarEventDescription = "Scheduled office cleaning day", CalendarEventDate = new DateOnly(2024, 01, 11), CalendarEventStartTime = new TimeOnly(09, 30), CalendarEventEndTime = new TimeOnly(10, 30) },
                new CalendarEvent() { CalendarEventId = 3, CalendarEventName = "Pottering", CalendarEventDescription = "Time for a relaxed atmosphere!", CalendarEventDate = new DateOnly(2024, 01, 11), CalendarEventStartTime = new TimeOnly(10, 30), CalendarEventEndTime = new TimeOnly(11, 30) },
                new CalendarEvent() { CalendarEventId = 4, CalendarEventName = "Project Meeting", CalendarEventDescription = "Team project meeting to discuss progress, challenges, and plans for project execution", CalendarEventDate = new DateOnly(2024, 01, 11), CalendarEventStartTime = new TimeOnly(11, 30), CalendarEventEndTime = new TimeOnly(12, 30) },
                new CalendarEvent() { CalendarEventId = 5, CalendarEventName = "Business Meeting", CalendarEventDescription = "Strategic business meeting covering company development, market strategy, and key decisions", CalendarEventDate = new DateOnly(2024, 01, 12), CalendarEventStartTime = new TimeOnly(12, 30), CalendarEventEndTime = new TimeOnly(13, 30) },
                new CalendarEvent() { CalendarEventId = 6, CalendarEventName = "Training Workshop", CalendarEventDescription = "Educational workshop aimed at enhancing employee's skills", CalendarEventDate = new DateOnly(2024, 01, 13), CalendarEventStartTime = new TimeOnly(13, 30), CalendarEventEndTime = new TimeOnly(14, 30) }
            );
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(
                new User() { Id = 1, Name = "Tomasz", Surname = "Tomaszewicz", Position = "Driver", Department = "Logistics", Login = "Log1", Password = "Pass1", AdminPrivilege = false},
                new User() { Id = 2, Name = "Andrzej", Surname = "Andrzejewski", Position = "Sales Manager", Department = "Global Services", Login = "Log2", Password = "Pass2", AdminPrivilege = false },
                new User() { Id = 3, Name = "Romek", Surname = "Romanowicz", Position = "Delivery Manager", Department = "Logistics", Login = "Log3", Password = "Pass2", AdminPrivilege = false },
                new User() { Id = 4, Name = "Zbigniew", Surname = "Zero", Position = "CEO", Department = "Head", Login = "Log4", Password = "Pass3", AdminPrivilege = true },
                new User() { Id = 5, Name = "Jordan", Surname = "Michael", Position = "Tester", Department = "IT", Login = "Log5", Password = "Pass4", AdminPrivilege = false },
                new User() { Id = 6, Name = "Marta", Surname = "Debowska", Position = "Supporter", Department = "IT", Login = "Log6", Password = "Pass5", AdminPrivilege = false }
            );
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

}
