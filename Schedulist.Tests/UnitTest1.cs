using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Schedulist.App.Controllers;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using System.Net.WebSockets;

namespace Schedulist.Tests
{
    public class UnitTest1
    {
        private readonly SchedulistDbContext db;
        private ILogger<BaseRepository> logger;

        
        [Fact]
        public void ReturnUserByIndexTest()
        {
            var userRepository = new UserRepository(db, logger);
            var expectedUID = 2;

            var result = userRepository.GetUserById(expectedUID.ToString());

            Assert.NotNull(result);
            Assert.Equal(expectedUID.ToString(), result.Id);
        }
        [Fact]
        public void CreateWorkModeForUserTest()
        {
            var workModeRepository = new WorkModeForUserRepository(db, logger);
            var initCount = workModeRepository.GetAllWorkModesForUser().Count();
            var newWorkModeForUser = new WorkModeForUser {DateOfWorkMode = DateOnly.Parse("2024-04-01"), UserId=1.ToString(),  WorkModeId=2};

            workModeRepository.CreateWorkModeForUser(newWorkModeForUser);
            var result = workModeRepository.GetAllWorkModesForUser();
            
            Assert.Equal(initCount+1, result.Count());
        }
        [Fact]
        public void EditCalendarEventNotExistingTest()
        {
            var calendarEventRepository = new CalendarEventRepository(db, logger);
            var updateEventId = 100;
            var updatedCalendarEvent = new CalendarEvent { Id = updateEventId, CalendarEventName="Application Testing", CalendarEventDescription="Calendar Event Application Testing"};

            calendarEventRepository.UpdateCalendarEvent(updateEventId, updatedCalendarEvent);
            var result = calendarEventRepository.GetCalendarEventById(updateEventId);

            Assert.Null(result);

        }
    }
}