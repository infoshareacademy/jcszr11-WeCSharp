using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schedulist.DAL.Models;
using Schedulist.DAL.Shared;

namespace Schedulist.DAL
{
    public class SchedulistDbContext : IdentityDbContext<User>
    {
        public SchedulistDbContext()
        {

        }
        public SchedulistDbContext(DbContextOptions<SchedulistDbContext> options) : base(options) { }

        public DbSet<WorkModeForUser> WorkModesToUsers { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<WorkMode> WorkModes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hasher = new PasswordHasher<User>();
            var hashedPassword = hasher.HashPassword(null, "Makrella1995!");

            builder.Entity<WorkMode>().HasData(
                new WorkMode() { Id = 1, Name = WorkModeNames.OFFICE },
                new WorkMode() { Id = 2, Name = WorkModeNames.HOME_OFFICE },
                new WorkMode() { Id = 3, Name = WorkModeNames.SICK_LEAVE },
                new WorkMode() { Id = 4, Name = WorkModeNames.DELEGATION },
                new WorkMode() { Id = 5, Name = WorkModeNames.HOLIDAY },
                new WorkMode() { Id = 6, Name = WorkModeNames.OTHERS}
            );
            builder.Entity<Position>().HasData(
                new Position() { Id = 1, Name = "Software Developer" },
                new Position() { Id = 2, Name = "Constructor" },
                new Position() { Id = 3, Name = "Human Resources Manager" },
                new Position() { Id = 4, Name = "Marketing Manager" },
                new Position() { Id = 5, Name = "CNC Operator" },
                new Position() { Id = 6, Name = "Financial Controller" },
                new Position() { Id = 7, Name = "Customer Service Supporter" },
                new Position() { Id = 8, Name = "Administrative Assistant" },
                new Position() { Id = 9, Name = "Procurement Specialist" },
                new Position() { Id = 10, Name = "Sales Representative" }
            );
            builder.Entity<Department>().HasData(
                new Department() { Id = 1, Name = "IT" },
                new Department() { Id = 2, Name = "Construction" },
                new Department() { Id = 3, Name = "Human Resources" },
                new Department() { Id = 4, Name = "Marketing" },
                new Department() { Id = 5, Name = "Production" },
                new Department() { Id = 6, Name = "Finance and Accounting" },
                new Department() { Id = 7, Name = "Customer Service" },
                new Department() { Id = 8, Name = "Administration" },
                new Department() { Id = 9, Name = "Procurement" },
                new Department() { Id = 10, Name = "Sales" }
             );
            builder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    Name = "Tomasz",
                    Surname = "Tomaszewicz",
                    UserName = "KURSTOMASZA@GMAIL.COM",
                    EmailConfirmed = true,
                    DepartmentId = 1,
                    PositionId = 2,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 50,
                    PasswordHash = hashedPassword
                }
            );
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "c3e92f9e-e8e9-4fe3-b600-ed1b055d25aa" },
                new IdentityRole { Name = "User", NormalizedName = "USER", Id = "bc877f1b-1e44-492c-acce-ba01f7bcd77f" }
            );
            builder.Entity<WorkModeForUser>().HasData(
                new WorkModeForUser() { Id = 1, DateOfWorkMode = new DateOnly(2024, 01, 10), WorkModeId = 1, UserId = "1" },
                new WorkModeForUser() { Id = 2, DateOfWorkMode = new DateOnly(2024, 01, 10), WorkModeId = 1, UserId = "2" },
                new WorkModeForUser() { Id = 3, DateOfWorkMode = new DateOnly(2024, 01, 11), WorkModeId = 1, UserId = "3" },
                new WorkModeForUser() { Id = 4, DateOfWorkMode = new DateOnly(2024, 01, 12), WorkModeId = 1, UserId = "1" },
                new WorkModeForUser() { Id = 5, DateOfWorkMode = new DateOnly(2024, 01, 13), WorkModeId = 1, UserId = "2" },
                new WorkModeForUser() { Id = 6, DateOfWorkMode = new DateOnly(2024, 01, 14), WorkModeId = 1, UserId = "2" }
            );

            builder.Entity<CalendarEvent>().HasData(
                new CalendarEvent() { Id = 1, CalendarEventName = "Maintenance Work", CalendarEventDescription = "Ongoing maintenance tasks in the office", CalendarEventDate = new DateOnly(2024, 01, 10), CalendarEventStartTime = new TimeOnly(08, 30, 0), CalendarEventEndTime = new TimeOnly(09, 30, 0), UserId = "1" },
                new CalendarEvent() { Id = 2, CalendarEventName = "Office Cleaning", CalendarEventDescription = "Scheduled office cleaning day", CalendarEventDate = new DateOnly(2024, 01, 11), CalendarEventStartTime = new TimeOnly(09, 30, 0), CalendarEventEndTime = new TimeOnly(10, 30, 0), UserId = "2" },
                new CalendarEvent() { Id = 3, CalendarEventName = "Pottering", CalendarEventDescription = "Time for a relaxed atmosphere!", CalendarEventDate = new DateOnly(2024, 01, 11), CalendarEventStartTime = new TimeOnly(10, 30, 0), CalendarEventEndTime = new TimeOnly(11, 30, 0), UserId = "1" },
                new CalendarEvent() { Id = 4, CalendarEventName = "Project Meeting", CalendarEventDescription = "Team project meeting to discuss progress, challenges, and plans for project execution", CalendarEventDate = new DateOnly(2024, 01, 11), CalendarEventStartTime = new TimeOnly(11, 30, 0), CalendarEventEndTime = new TimeOnly(12, 30, 0), UserId = "3" },
                new CalendarEvent() { Id = 5, CalendarEventName = "Business Meeting", CalendarEventDescription = "Strategic business meeting covering company development, market strategy, and key decisions", CalendarEventDate = new DateOnly(2024, 01, 12), CalendarEventStartTime = new TimeOnly(12, 30, 0), CalendarEventEndTime = new TimeOnly(13, 30, 0), UserId = "2" },
                new CalendarEvent() { Id = 6, CalendarEventName = "Training Workshop", CalendarEventDescription = "Educational workshop aimed at enhancing employee's skills", CalendarEventDate = new DateOnly(2024, 01, 13), CalendarEventStartTime = new TimeOnly(13, 30, 0), CalendarEventEndTime = new TimeOnly(14, 30, 0), UserId = "1" }
            );


        }
    }

}
