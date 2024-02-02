using Schedulist.App.Models.Domain_Models;

namespace Schedulist.App.Helper
{
    public static class WorkModeNamesList
    {
        public static List<WorkModeName> GetAll() 
        {
            return new List<WorkModeName> 
            {
                new WorkModeName() {Name = "Office", Id = "Office" },
                new WorkModeName() {Name = "Home office", Id= "Home office"},
                new WorkModeName() {Name = "Sick leave", Id = "Sick leave"},
                new WorkModeName() {Name = "Holiday leave", Id = "Holiday leave"},
                new WorkModeName() {Name = "Another work mode", Id = "Another work mode"}
            };
        }
    }
}
